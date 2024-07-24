if exist .\ContactBook\AppData\contact-database.db (
    del .\ContactBook\AppData\contact-database.db
)

if not exist .\ContactBook\AppData (
    mkdir .\ContactBook\AppData
)

sqlite3 .\ContactBook\AppData\contact-database.db < recreate-database.sql & pause
