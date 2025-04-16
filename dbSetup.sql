
-- NOTE you will need to create this table today
-- you must send a GET request to the accounts endpoint with your bearer token in order to add your user to the sql database
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL PRIMARY KEY COMMENT 'primary key',
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name VARCHAR(255) COMMENT 'User Name',
  email VARCHAR(255) UNIQUE COMMENT 'User Email',
  picture VARCHAR(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';

CREATE TABLE IF NOT EXISTS cars(
  -- NOTE make sure your id column is the first column you define
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  make TINYTEXT NOT NULL,
  model TINYTEXT NOT NULL,
  year INT UNSIGNED NOT NULL,
  color TINYTEXT NOT NULL,
  price MEDIUMINT UNSIGNED NOT NULL,
  mileage MEDIUMINT UNSIGNED NOT NULL,
  engine_type ENUM('small', 'medium', 'large', 'super-size', 'battery'),
  img_url TEXT NOT NULL,
  has_clean_title BOOLEAN NOT NULL DEFAULT true,
  creator_id VARCHAR(255) NOT NULL,
  -- NOTE this will validate that an actual id for an account was used when INSERTING a car into the data base
  -- this will also delete an accounts created cars if the user deletes their account
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);

INSERT INTO 
cars (make, model, year, price, color, mileage, engine_type, img_url, has_clean_title, creator_id)
VALUES ('honda', 's2000', 2008, 20000, 'silver', 200000, 'medium', 'https://images.unsplash.com/photo-1723407338018-709fbf9ed494?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fHMyMDAwfGVufDB8fDB8fHww', false, '670ff93326693293c631476f');

SELECT * FROM accounts;

SELECT * FROM cars;

-- NOTE JOIN is how we include multiple rows of data on the same row
-- INNER JOIN denotes that there must be a match between the two columns, or no data is returned
-- ON tells our database when to match up data, otherwise it will match everything to everything
SELECT 
cars.*,
accounts.*
FROM cars
INNER JOIN accounts ON accounts.id = cars.creator_id;


SELECT 
cars.*,
accounts.*
FROM cars
INNER JOIN accounts ON accounts.id = cars.creator_id
WHERE cars.id = 3;


UPDATE cars SET make = "mazda", model = "miata" WHERE id = 5 LIMIT 1;



CREATE TABLE IF NOT EXISTS houses(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  sqft SMALLINT UNSIGNED NOT NULL,
  bedrooms TINYINT UNSIGNED NOT NULL,
  bathrooms DOUBLE UNSIGNED NOT NULL,
  img_url VARCHAR(1000) NOT NULL,
  description VARCHAR(500),
  price INT UNSIGNED NOT NULL,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Time Last Updated At',
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);

DROP TABLE houses

INSERT INTO 
houses (sqft, bedrooms, bathrooms, img_url, description, price, creator_id)
VALUES (2000, 2, 2, 'https://plus.unsplash.com/premium_photo-1689609950112-d66095626efb?q=80&w=1974&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', 'Cozy little place. Nice neighborhood, good schools, businesses nearby.', 500000, '67e325c8d0ea7c2e5c72e705')

SELECT * FROM houses