"""
Connect to database using ODBC DSN

"""

import logging
import contextlib
import pyodbc

from config import DRIVER, SERVER_NAME, DATABASE, USERNAME, PASSWORD, SCHEMA, PORT

my_logger = logging.getLogger(__name__)


def get_db_connection_dsn():
    """
    Get connection to MSSQL database using DSN
    :return:
    """
    try:
        db_connection = pyodbc.connect(
            'DRIVER=' + DRIVER + ';SERVER=' + SERVER_NAME + ';PORT=1433;DATABASE=' + DATABASE + ';UID=' + USERNAME + ';PWD=' + PASSWORD)
        return db_connection
    except pyodbc.Error as e:
        my_logger.warning("Could not Connect to ODBC Database")
        my_logger.error(f"db_server: {SERVER_NAME}, db_port: {PORT}, db_name: {DATABASE}, db_user: {USERNAME}, db_password: {PASSWORD} ")
        print(str(e))
        return None

def get_db_connection():
    """
    Get connection to MSSQL database
    :return:
    """
    try:
        db_connection = pyodbc.connect('Driver={SQL Server};'
                      'Server=MASERATI;'
                      'Database=Asset;'
                      'Trusted_Connection=yes;')
        return db_connection
    except pyodbc.Error as e:
        my_logger.warning("Could not Connect to Database")
        my_logger.error(f"db_server: {SERVER_NAME}, db_port: {PORT}, db_name: {DATABASE}, db_user: {USERNAME}, db_password: {PASSWORD} ")
        print(str(e))
        return None

def test_connection():
    conn = get_db_connection()
    cursor = conn.cursor()
    cursor.execute('SELECT * FROM Asset.dbo.Company')
    for row in cursor:
        print(row)

def bulk_insert(table_name='[' + SCHEMA + '].[Airports]',
                file_path=r'D:\main\code\Python\activate\in\airports.txt'):
    pyodbc.connect('Driver={SQL Server};'
                      'Server=MASERATI;'
                      'Database=Asset;'
                      'Trusted_Connection=yes;')
    string = "BULK INSERT {} FROM '{}' (WITH FORMAT = 'CSV');"
    with contextlib.closing(pyodbc.connect("MYCONN")) as conn:
        with contextlib.closing(conn.cursor()) as cursor:
            cursor.execute(string.format(table_name, file_path))
        conn.commit()
        conn.close()

