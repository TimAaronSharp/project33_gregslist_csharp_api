
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
VALUES ('honda', 's2000', 2008, 20000, 'silver', 200000, 'medium', 'https://images.unsplash.com/photo-1723407338018-709fbf9ed494?w=500&auto=format&fit=crop&q=60&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxzZWFyY2h8MTZ8fHMyMDAwfGVufDB8fDB8fHww', false, '67e3273fee37d52171a8018c');

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
  someone_died BOOLEAN DEFAULT false,
  is_haunted BOOLEAN DEFAULT false,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);

DROP TABLE houses

INSERT INTO 
houses (sqft, bedrooms, bathrooms, img_url, description, price, someone_died, is_haunted, creator_id)
VALUES (3000, 4, 3, 'https://images.unsplash.com/photo-1481018085669-2bc6e4f00eed?q=80&w=2070&auto=format&fit=crop&ixlib=rb-4.0.3&ixid=M3wxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8fA%3D%3D', "DEFINITELY not haunted and nobody died here. Don't listen to Sharon.", 200000, true, true,'67e3273fee37d52171a8018c')

SELECT * FROM houses

SELECT houses.*, accounts.* FROM houses INNER JOIN accounts ON accounts.id = houses.creator_id

CREATE TABLE IF NOT EXISTS jobs(
  id INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
  company_name VARCHAR(255) NOT NULL,
  job_title VARCHAR(255) NOT NULL,
  salary INT UNSIGNED NOT NULL,
  description VARCHAR(1000) NOT NULL,
  site_location VARCHAR(255) NOT NULL,
  company_headquarters VARCHAR(255) NOT NULL,
  is_remote BOOLEAN DEFAULT false,
  sucks BOOLEAN DEFAULT false,
  created_at DATETIME DEFAULT CURRENT_TIMESTAMP,
  updated_at DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  creator_id VARCHAR(255) NOT NULL,
  FOREIGN KEY (creator_id) REFERENCES accounts(id) ON DELETE CASCADE
);

DROP TABLE jobs

INSERT INTO
jobs (company_name, job_title, salary, description, site_location, company_headquarters, is_remote, sucks, creator_id)
VALUES ("Joe's Taxidermy", "Skin Master", 30000, "You snuff 'em, we stuff em!", "That shady place off 9000 South and Bangerter", "A non-extradition country.", true, true, "67e325c8d0ea7c2e5c72e705");

INSERT INTO
jobs (company_name, job_title, salary, description, site_location, company_headquarters, is_remote, sucks, creator_id)
VALUES ("Underground Emo Music Records", "Chief Edgelord", 25000, "You've probably never heard of it.", "It's not really underground, stupid.", "Ok, this one is underground.", true, false, "67e325c8d0ea7c2e5c72e705");

SELECT * FROM jobs

UPDATE jobs
    SET
    company_name = @CompanyName,
    job_title = @JobTitle,
    salary = @Salary,
    description = @Description,
    site_location = @SiteLocation,
    company_headquarters = @CompanyHeadquarters,
    is_remote = @IsRemote,
    sucks = @Sucks,
    creator_id = @CreatorId
    WHERE id = @Id
    LIMIT 1;