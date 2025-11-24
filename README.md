## Инстркуция

Приложение разработано с помощью C# ASP.NET Core с использованием Entity Framework Core и СУБД MicrosoftSQL


# Подготовка к запуску

Для работы необходима база данных. Название, и учетная запись должны быть указаны в `appsettings.json`. Все необходимые таблицы будут созданы автоматически во время работы программы.

Запрос, который использовался для создания БД и учётной записи:<br>
CREATE DATABASE [taskdb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'taskdb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\DATA\taskdb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'taskdb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL17.MSSQLSERVER\MSSQL\DATA\taskdb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB );<br>
CREATE LOGIN doc with password='password';<br>
create user doc for login doc;<br>
exec sp_addrolemember 'db_owner', 'doc';<br>

Строчка в `appsettings.json`, задающая БД, с которой работает программа<br>
"ConnectionStrings": {
    "MainDb": "Server=localhost;Initial Catalog=taskdb;User Id=doc;Password=password;TrustServerCertificate=True"
}`

# Запуск
1. Необходимо запустить файл `Mvc_WebbApp\Mvc_WebbApp.exe`.
2. Открыть в браузере `http://localhost:5000`. Пользователь попадёт на домашнюю страницу, где описано техническое задание и функции приложения.


