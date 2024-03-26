import json
import os
from pathlib import Path
 
dsn_config = Path(os.environ["BUILD_SOURCESDIRECTORY"]).joinpath("AzureIaas", "AzDevOps", "IaaS", "Common", "dsnConfig.json")
 
dsn_config_json = json.loads(dsn_config.read_text())
default_driver = "ODBC Driver 17 for SQL Server"
valid_drivers = { "ODBC Driver 17 for SQL Server", "ODBC Driver 18 for SQL Server"}
 
odbc_text = ""
 
for server_settings in dsn_config_json["ODBC"]:
    server = server_settings["server"]
    driver = server_settings["driver"]
    if driver not in valid_drivers:
        driver = default_driver
    
    for dsn in server_settings["dsns"]:
        dsn_name = dsn["name"]
        dsn_database = dsn["database"]
        dsn_text = f"""
[{dsn_name}]
Driver={driver}
Server={server}
Database={dsn_database}
Trusted_Connection=yes
Encrypt=no
TrustServerCertificate=no
 
"""
        odbc_text += dsn_text
        
odbc_ini = Path(os.environ["BUILD_SOURCESDIRECTORY"]).joinpath("jupyterhub", "aks", "scripts", "odbc.ini")
odbc_ini.unlink(missing_ok=True)
odbc_ini.write_text(odbc_text, encoding="utf-8")
 