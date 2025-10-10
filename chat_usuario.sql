-- MySQL dump 10.13  Distrib 8.0.42, for Win64 (x86_64)
--
-- Host: localhost    Database: chat
-- ------------------------------------------------------
-- Server version	8.0.42

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `grupos`
--

DROP TABLE IF EXISTS `grupos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `grupos` (
  `id_grupo` int NOT NULL AUTO_INCREMENT,
  `nombre_grupo` varchar(100) NOT NULL,
  `fecha_creacion` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  `creador_id` int DEFAULT NULL,
  PRIMARY KEY (`id_grupo`),
  UNIQUE KEY `nombre_grupo` (`nombre_grupo`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `grupos`
--

LOCK TABLES `grupos` WRITE;
/*!40000 ALTER TABLE `grupos` DISABLE KEYS */;
INSERT INTO `grupos` VALUES (1,'TheBest','2025-10-10 03:50:57',1),(2,'Sistemas','2025-10-10 03:51:19',1),(3,'Como ','2025-10-10 04:11:21',NULL);
/*!40000 ALTER TABLE `grupos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `mensajes`
--

DROP TABLE IF EXISTS `mensajes`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `mensajes` (
  `id` int NOT NULL AUTO_INCREMENT,
  `remitente` varchar(100) NOT NULL,
  `destinatario` varchar(100) DEFAULT NULL,
  `grupo` varchar(100) DEFAULT NULL,
  `mensaje` text NOT NULL,
  `fecha` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id`),
  KEY `idx_remitente` (`remitente`),
  KEY `idx_destinatario` (`destinatario`),
  KEY `idx_grupo` (`grupo`),
  KEY `idx_fecha` (`fecha`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `mensajes`
--

LOCK TABLES `mensajes` WRITE;
/*!40000 ALTER TABLE `mensajes` DISABLE KEYS */;
INSERT INTO `mensajes` VALUES (1,'Hola57',NULL,NULL,'Hola?','2025-10-10 03:49:43'),(2,'Jacob57',NULL,NULL,'hola','2025-10-10 04:10:53');
/*!40000 ALTER TABLE `mensajes` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `miembros_grupo`
--

DROP TABLE IF EXISTS `miembros_grupo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `miembros_grupo` (
  `id_miembro` int NOT NULL AUTO_INCREMENT,
  `id_grupo` int DEFAULT NULL,
  `id_usuario` int DEFAULT NULL,
  `fecha_union` timestamp NULL DEFAULT CURRENT_TIMESTAMP,
  PRIMARY KEY (`id_miembro`),
  KEY `id_grupo` (`id_grupo`),
  KEY `id_usuario` (`id_usuario`),
  CONSTRAINT `miembros_grupo_ibfk_1` FOREIGN KEY (`id_grupo`) REFERENCES `grupos` (`id_grupo`),
  CONSTRAINT `miembros_grupo_ibfk_2` FOREIGN KEY (`id_usuario`) REFERENCES `usuario` (`id_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=5 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `miembros_grupo`
--

LOCK TABLES `miembros_grupo` WRITE;
/*!40000 ALTER TABLE `miembros_grupo` DISABLE KEYS */;
INSERT INTO `miembros_grupo` VALUES (1,1,NULL,'2025-10-10 03:50:57'),(2,2,NULL,'2025-10-10 03:51:19'),(3,3,8,'2025-10-10 04:11:21'),(4,3,3,'2025-10-10 04:11:21');
/*!40000 ALTER TABLE `miembros_grupo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuario` (
  `id_usuario` int NOT NULL AUTO_INCREMENT,
  `nombre` varchar(45) NOT NULL,
  `password` varchar(80) NOT NULL,
  `usuario` varchar(50) NOT NULL,
  PRIMARY KEY (`id_usuario`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'Carlos','Pqnoe+r6BKGnkd5eSDbEK4ZcUr4X2uzh/FoSvXtf1p7ltXME','Hola57'),(2,'Juan','MfumQ8mj7wEsa0681QvDPwWelEdeOeTlBHlG3DDzhxuSz+1W','Carlos57'),(3,'Maria','zb3gPP/mx1gHN0yvDL1IgOm0i+9QIcWnlav425jZOPl70fj/','Mari75'),(4,'Carlos','0Ex0Mq09VTgZRyDzzoj+MYaGie9wFRf5lu/GZvOrzVzY09jj','Carlos78'),(5,'Juan','iMsIOr2pFXS2hXHocx8uB9linetbknhZZrnMTgrs9GXu1IkG','Juan78'),(6,'Maria','ShQr0soMNR8mKKl1JzQitF7ZByW5haYex2/Tma5mlL3+O8Kv','Mari78'),(7,'Maria','7Z1cdmiV4Gl3z6wLSwsNvky5ByxBOoFjY7DoRfdTy/NQ0WaW','Maria79'),(8,'Redvastion','9bg83tbJOhZjuTv1HyS+wau7f+unUVqSJ5vNYWErGnhKpraM','Jacob57'),(9,'Juan','fDPCnTutlXK6/6yHxYxHA18Y114bIaZoIVOCMpnFNMB+fs8L','Carlos578');
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2025-10-09 22:17:30
