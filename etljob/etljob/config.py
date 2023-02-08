"""
Contains all Configuration Information 

"""
import logging
import os

###############################################
# Environment Variables Section
###############################################

# Uncomment below code-block for runnning it on local
# For Production, add these as variables in Application settings for the webjob

os.environ['db_driver'] = '{ODBC Driver 17 for SQL Server}'  # For Webjob, set it to 17 ie. '{ODBC Driver 17 for SQL Server}'
os.environ['db_server'] = 'MASERATI'
os.environ['db_port'] = '1433'
os.environ['db_user'] = 'sa'
os.environ['db_password'] = 'journey'
os.environ['db_schema'] = 'dbo'
os.environ['db_name'] = 'Asset'

DRIVER = os.getenv('db_driver')  # For Webjob, set it to 17 ie. '{ODBC Driver 17 for SQL Server}'
PORT = os.getenv('db_port')

SERVER_NAME = os.getenv('db_server')
DATABASE = os.getenv('db_name')
USERNAME = os.getenv('db_user')
PASSWORD = os.getenv('db_password')
SCHEMA = os.getenv('db_schema')

# Path for spark source folder
os.environ['SPARK_HOME']="D:\main\prog\spark-2.4.1-bin-hadoop2.7"

###############################################
# Logging Section
###############################################

# set up logging to file
LOGS_DIR = os.path.join(os.path.dirname(os.path.realpath(__file__)), "run_logs")
if not os.path.exists(LOGS_DIR):
    os.makedirs(LOGS_DIR)

logging.basicConfig(level=logging.INFO,
                    # format='%(asctime)s %(name)-12s %(levelname)-8s %(message)s',
                    format="[%(levelname)-8s %(asctime)s %(filename)s:%(lineno)s - %(funcName)20s()] %(message)s",
                    datefmt='%m-%d %H:%M',
                    filename=os.path.join(LOGS_DIR, "lastrun.log"),
                    filemode='w')

