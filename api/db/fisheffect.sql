-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema fisheffect
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema fisheffect
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `fisheffect` DEFAULT CHARACTER SET utf8 ;
USE `fisheffect` ;

-- -----------------------------------------------------
-- Table `fisheffect`.`fish_for_sale`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `fisheffect`.`fish_for_sale` (
  `hashFish` VARCHAR(127) NOT NULL,
  `hashReef` VARCHAR(127) NULL,
  `value` INT NULL,
  PRIMARY KEY (`hashFish`))
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
