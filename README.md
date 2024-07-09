# Курсовой проект "Электронный каталог CD-дисков"

Инструменты и технологии:
1. Visual Studio 2022
2. C# .NET 8
3. MSSQL Server 2022

Возможности:
1. Регистрация и авторизация в системе по ролям:
   * Администратор
   * Пользователь
   * Гость
2. Просмотр товаров без авторизации
3. Авторизованные пользователи могут добавлять желаемые товары в корзину
4. Администратор может удалять, изменять, добавлять, редактировать записи в базе данных

Установка:
1. Склонировать репозиторий
2. Выполнить скрипт создания БД в MSSQL
3. Открыть проект в Visual Studio
4. В классе DataBase, в строке подключения прописать название сервера в Data Source 
5. Создать модель данных ADO.NET нашей БД
6. В Model.Context.tt\Model.Context.cs (название модели может отличаться), прописать private static CDstoreEntities2(название сущности может отличаться) _context;
7. В том же файле прописать:  
public static CDstoreEntities2 GetContext()
{
    if(_context == null)
        _context = new CDstoreEntities2();
    return _context;
}
8. Запустить проект
