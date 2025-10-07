-- 🧨 Eliminar base de datos si ya existe (¡CUIDADO! Borra todo)
DROP DATABASE IF EXISTS chat;

-- 🛠 Crear base de datos con soporte para acentos y emojis
CREATE DATABASE chat CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci;
USE chat;

-- 🧹 Eliminar tabla si existe
DROP TABLE IF EXISTS usuario;

-- 📦 Crear tabla de usuarios
CREATE TABLE usuario (
  id_usuario INT NOT NULL AUTO_INCREMENT,
  nombre VARCHAR(45) NOT NULL,
  password VARCHAR(80) NOT NULL,
  usuario VARCHAR(50) NOT NULL,
  PRIMARY KEY (id_usuario)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;

-- 👥 Insertar usuarios de prueba
INSERT INTO usuario (nombre, password, usuario) VALUES
('Carlos','Pqnoe+r6BKGnkd5eSDbEK4ZcUr4X2uzh/FoSvXtf1p7ltXME','Hola57'),
('Juan','MfumQ8mj7wEsa0681QvDPwWelEdeOeTlBHlG3DDzhxuSz+1W','Carlos57'),
('Maria','zb3gPP/mx1gHN0yvDL1IgOm0i+9QIcWnlav425jZOPl70fj/','Mari75'),
('Carlos','0Ex0Mq09VTgZRyDzzoj+MYaGie9wFRf5lu/GZvOrzVzY09jj','Carlos78'),
('Juan','iMsIOr2pFXS2hXHocx8uB9linetbknhZZrnMTgrs9GXu1IkG','Juan78'),
('Maria','ShQr0soMNR8mKKl1JzQitF7ZByW5haYex2/Tma5mlL3+O8Kv','Mari78'),
('Maria','7Z1cdmiV4Gl3z6wLSwsNvky5ByxBOoFjY7DoRfdTy/NQ0WaW','Maria79');

-- 🔐 Eliminar usuario si ya existe
DROP USER IF EXISTS 'chatuser'@'localhost';

-- 🧑‍💻 Crear usuario de conexión con método compatible
CREATE USER 'chatuser'@'localhost' IDENTIFIED WITH mysql_native_password BY 'S3bas!2025_DBchat';

-- 🛡 Otorgar permisos necesarios
GRANT SELECT, INSERT, UPDATE, DELETE ON chat.* TO 'chatuser'@'localhost';
FLUSH PRIVILEGES;
