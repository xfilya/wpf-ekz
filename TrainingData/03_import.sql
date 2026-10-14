INSERT INTO categories(name)
SELECT DISTINCT "Категория" FROM Products_import;
INSERT INTO manufacturers(name)
SELECT DISTINCT "Производство" FROM Products_import;
INSERT INTO roles(name)
SELECT DISTINCT "Роль" FROM Users_import;

INSERT INTO users(login, first_name, last_name, patronymic, role_id)
SELECT u."Логин", u."Имя", u."Фамилия", u."Отчетсво", r.id
FROM Users_import AS u
JOIN roles AS r ON u."Роль" = r.name;

INSERT INTO products(name, category_id, manufacturer_id,
 description, composition, price, image_file_name)
SELECT p."Наименование товара", c.id, m.id,
 p."Описание", p."Состав", p."Цена", p."Изображение"
FROM Products_import AS p
JOIN categories AS c ON p."Категория" = c.name
JOIN manufacturers AS m ON p."Производство" = m.name;

INSERT INTO stock_items(product_id, quantity, size)
SELECT p.id, s."Количество доступное для заказа", s."Размер"
FROM Stock_Items_import AS s
JOIN manufacturers AS m ON s."Производство" = m.name
JOIN products AS p ON s."Наименование товара" = p.name
 AND p.manufacturer_id = m.id;

INSERT INTO orders(id, order_date, user_id)
SELECT DISTINCT o."Номер заказа", o."Дата заказа", u.id
FROM Orders_import AS o
JOIN users AS u ON o."ФИО" =
 u.last_name || ' ' || u.first_name || ' ' || u.patronymic;

INSERT INTO order_items(order_id, stock_item_id,
 quantity, unit_price)
SELECT o."Номер заказа", s.id,
 o."Количество", o."Цена за единицу"
FROM Orders_import AS o
JOIN manufacturers AS m ON o."Производство" = m.name
JOIN products AS p ON o."Наименование товара" = p.name
 AND p.manufacturer_id = m.id
JOIN stock_items AS s ON s.product_id = p.id
 AND s.size = o."Размер";
