"""
Python Startup Main Script File

"""

# import python include files and configuration settings
import os
import sys
import logging
import config

logging.basicConfig(format='%(asctime)s - %(message)s', datefmt='%d-%b-%y %H:%M:%S', level=logging.DEBUG)

from db_mssql_utility import get_db_connection

# Append pyspark  to Python Path
sys.path.append("D:\main\prog\spark-2.4.1-bin-hadoop2.7\python\pyspark")

def check_database():
#   Step 1 - Check to see if database connection exists
    conn = get_db_connection()
    if not conn:
        logging.error("Could not connect to Database")
        sys.exit(-1)

def check_spark():
#   Step 2 - Check to see if spark libraries are available
    try:
        from pyspark import SparkContext
        from pyspark import SparkConf
        print("Successfully imported Spark Modules")  
    except ImportError as e:
        print("Can not import Spark Modules", e)
        sys.exit(-1)

def check_all():
    check_database()
    check_spark()

if __name__ == '__main__':
    check_all() # run script environment validation checks
