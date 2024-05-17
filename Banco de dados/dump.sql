-- MySQL dump 10.13  Distrib 8.0.36, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: db_evento
-- ------------------------------------------------------
-- Server version	8.0.36

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
-- Table structure for table `evento`
--

DROP TABLE IF EXISTS `evento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `evento` (
  `id` int NOT NULL AUTO_INCREMENT,
  `descricao` varchar(180) COLLATE utf8mb3_bin NOT NULL,
  `data_evento` datetime NOT NULL,
  `imagem_url` varchar(90) COLLATE utf8mb3_bin DEFAULT NULL,
  `local` varchar(45) COLLATE utf8mb3_bin NOT NULL,
  `ativo` bit(1) NOT NULL,
  `nome_evento` varchar(45) COLLATE utf8mb3_bin NOT NULL,
  `total_ingressos` int DEFAULT NULL,
  `image` mediumblob,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `evento`
--

LOCK TABLES `evento` WRITE;
/*!40000 ALTER TABLE `evento` DISABLE KEYS */;
INSERT INTO `evento` VALUES (1,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',420,NULL),(2,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',330,NULL),(3,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',300,NULL),(4,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',300,NULL),(5,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',350,NULL),(6,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',1200,NULL),(7,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',500,NULL),(8,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',600,NULL),(9,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',600,NULL),(10,'Descrição Teste','2024-05-03 00:00:00','','Local evento teste',_binary '','Nome teste',1200,NULL);
/*!40000 ALTER TABLE `evento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `ingressos`
--

DROP TABLE IF EXISTS `ingressos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `ingressos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `pedidos_id` int NOT NULL,
  `pedidos_usuarios_id` int NOT NULL,
  `lote_id` int NOT NULL,
  `status` varchar(45) COLLATE utf8mb3_bin DEFAULT NULL,
  `tipo` varchar(45) COLLATE utf8mb3_bin DEFAULT NULL,
  `data_utilizacao` datetime DEFAULT NULL,
  `ativo` bit(1) DEFAULT NULL,
  `valor` double DEFAULT NULL,
  `codigo_qr` varchar(50) COLLATE utf8mb3_bin NOT NULL,
  PRIMARY KEY (`id`,`pedidos_id`,`pedidos_usuarios_id`,`lote_id`,`codigo_qr`),
  KEY `fk_ingressos_pedidos1_idx` (`pedidos_id`,`pedidos_usuarios_id`) /*!80000 INVISIBLE */,
  KEY `fk_ingressos_lote1_idx` (`lote_id`) /*!80000 INVISIBLE */,
  CONSTRAINT `fk_ingressos_lote1` FOREIGN KEY (`lote_id`) REFERENCES `lote` (`id`),
  CONSTRAINT `fk_ingressos_pedidos1` FOREIGN KEY (`pedidos_id`, `pedidos_usuarios_id`) REFERENCES `pedidos` (`id`, `usuarios_id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `ingressos`
--

LOCK TABLES `ingressos` WRITE;
/*!40000 ALTER TABLE `ingressos` DISABLE KEYS */;
INSERT INTO `ingressos` VALUES (4,2,1,31,'Pendente','Colaborador','2024-05-15 00:00:00',_binary '',20,''),(5,2,1,31,'Pendente','Colaborador','2024-05-15 00:00:00',_binary '',20,''),(6,2,1,31,'Pendente','Colaborador','2024-05-15 00:00:00',_binary '',20,''),(7,2,1,31,'Pendente','Aluno','2024-05-15 00:00:00',_binary '',20,''),(8,2,1,31,'Pendente','Aluno','2024-05-15 00:00:00',_binary '',20,''),(9,2,1,31,'Pendente','Colaborador','2024-05-15 00:00:00',_binary '',20,'');
/*!40000 ALTER TABLE `ingressos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lote`
--

DROP TABLE IF EXISTS `lote`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `lote` (
  `id` int NOT NULL AUTO_INCREMENT,
  `evento_id` int NOT NULL,
  `valor_unitario` double DEFAULT NULL,
  `quantidade_total` int DEFAULT NULL,
  `saldo` int DEFAULT NULL,
  `data_inicio` datetime DEFAULT NULL,
  `data_final` datetime DEFAULT NULL,
  `ativo` bit(1) DEFAULT NULL,
  `tipo` set('quantidade','tempo','gratis') COLLATE utf8mb3_bin DEFAULT NULL,
  `nome` varchar(45) COLLATE utf8mb3_bin DEFAULT NULL,
  PRIMARY KEY (`id`,`evento_id`),
  KEY `fk_lote_pedidos_ingressos1_idx` (`evento_id`),
  CONSTRAINT `fk_lote_pedidos_ingressos1` FOREIGN KEY (`evento_id`) REFERENCES `evento` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=32 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lote`
--

LOCK TABLES `lote` WRITE;
/*!40000 ALTER TABLE `lote` DISABLE KEYS */;
INSERT INTO `lote` VALUES (31,5,10,100,100,'2024-05-15 00:00:00','2024-05-15 00:00:00',_binary '','quantidade','lote 1');
/*!40000 ALTER TABLE `lote` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pedidos`
--

DROP TABLE IF EXISTS `pedidos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `pedidos` (
  `id` int NOT NULL AUTO_INCREMENT,
  `usuarios_id` int NOT NULL,
  `data` datetime DEFAULT NULL,
  `total` double DEFAULT NULL,
  `quantidade` int DEFAULT NULL,
  `forma_pagamento` varchar(45) COLLATE utf8mb3_bin DEFAULT NULL,
  `status` varchar(20) COLLATE utf8mb3_bin DEFAULT 'Pendente',
  `validacao_id_usuario` int DEFAULT '0',
  PRIMARY KEY (`id`,`usuarios_id`),
  KEY `fk_pedidos_usuarios_idx` (`usuarios_id`),
  CONSTRAINT `fk_pedidos_usuarios` FOREIGN KEY (`usuarios_id`) REFERENCES `usuarios` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pedidos`
--

LOCK TABLES `pedidos` WRITE;
/*!40000 ALTER TABLE `pedidos` DISABLE KEYS */;
INSERT INTO `pedidos` VALUES (2,1,'2024-05-15 00:00:00',10,1,'Pix','Pendente',0);
/*!40000 ALTER TABLE `pedidos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuarios`
--

DROP TABLE IF EXISTS `usuarios`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `usuarios` (
  `id` int NOT NULL,
  `nome_completo` varchar(100) COLLATE utf8mb3_bin DEFAULT NULL,
  `email` varchar(100) COLLATE utf8mb3_bin DEFAULT NULL,
  `senha` varchar(45) COLLATE utf8mb3_bin DEFAULT NULL,
  `telefone` varchar(30) COLLATE utf8mb3_bin DEFAULT NULL,
  `perfil` varchar(100) COLLATE utf8mb3_bin DEFAULT NULL,
  `ativo` bit(1) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb3 COLLATE=utf8mb3_bin;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuarios`
--

LOCK TABLES `usuarios` WRITE;
/*!40000 ALTER TABLE `usuarios` DISABLE KEYS */;
INSERT INTO `usuarios` VALUES (1,'Nome 1 ','email@email.com','123456','14996673666','Usuario',_binary '');
/*!40000 ALTER TABLE `usuarios` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-05-17  8:33:58
