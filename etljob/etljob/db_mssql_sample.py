"""
Sample Class for working with MS SQL Server 

"""

import pyodbc 
conn = pyodbc.connect('Driver={SQL Server};'
                      'Server=MASERATI;'
                      'Database=Asset;'
                      'Trusted_Connection=yes;')

cursor = conn.cursor()
cursor.execute('SELECT * FROM Asset.dbo.Company')

for row in cursor:
    print(row)
