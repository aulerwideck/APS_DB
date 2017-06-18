-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema aps
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema aps
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `aps` DEFAULT CHARACTER SET utf8 ;
USE `aps` ;

-- -----------------------------------------------------
-- Table `aps`.`TipoPessoa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`TipoPessoa` (
  `idTipoPessoa` INT NOT NULL AUTO_INCREMENT,
  `Descricao` VARCHAR(20) NOT NULL,
  PRIMARY KEY (`idTipoPessoa`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Pessoa`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Pessoa` (
  `idPessoa` INT NOT NULL AUTO_INCREMENT,
  `idTipoPessoa` INT NOT NULL,
  `RazaoSocial` VARCHAR(45) NOT NULL,
  `CpfCnpj` VARCHAR(14) NOT NULL,
  `RG` VARCHAR(11) NULL,
  `IE` VARCHAR(15) NULL,
  `Email` VARCHAR(45) NULL,
  `DataNasc` DATE NULL,
  PRIMARY KEY (`idPessoa`),
  INDEX `fk_Pessoa_TipoPessoa_idx` (`idTipoPessoa` ASC),
  CONSTRAINT `fk_Pessoa_TipoPessoa`
    FOREIGN KEY (`idTipoPessoa`)
    REFERENCES `aps`.`TipoPessoa` (`idTipoPessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Pais`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Pais` (
  `idPais` INT NOT NULL AUTO_INCREMENT,
  `Sigla` VARCHAR(3) NOT NULL,
  `Nome` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idPais`),
  UNIQUE INDEX `Nome_UNIQUE` (`Nome` ASC),
  UNIQUE INDEX `Sigla_UNIQUE` (`Sigla` ASC),
  UNIQUE INDEX `idPais_UNIQUE` (`idPais` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Estado`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Estado` (
  `idEstado` INT NOT NULL AUTO_INCREMENT,
  `idPais` INT NOT NULL,
  `Sigla` CHAR(2) NOT NULL,
  `Nome` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idEstado`),
  INDEX `fk_Estado_Pais1_idx` (`idPais` ASC),
  CONSTRAINT `fk_Estado_Pais1`
    FOREIGN KEY (`idPais`)
    REFERENCES `aps`.`Pais` (`idPais`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Cidade`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Cidade` (
  `idCidade` INT NOT NULL AUTO_INCREMENT,
  `idEstado` INT NOT NULL,
  `Nome` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idCidade`),
  INDEX `fk_Cidade_Estado1_idx` (`idEstado` ASC),
  CONSTRAINT `fk_Cidade_Estado1`
    FOREIGN KEY (`idEstado`)
    REFERENCES `aps`.`Estado` (`idEstado`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Endereco`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Endereco` (
  `idEndereco` INT NOT NULL AUTO_INCREMENT,
  `idPessoa` INT NOT NULL,
  `idCidade` INT NOT NULL,
  `Rua` VARCHAR(45) NOT NULL,
  `Numero` INT NOT NULL,
  `Complemento` VARCHAR(45) NULL,
  `Bairro` VARCHAR(45) NOT NULL,
  `CEP` CHAR(9) NOT NULL,
  PRIMARY KEY (`idEndereco`),
  INDEX `fk_Endereco_Pessoa1_idx` (`idPessoa` ASC),
  INDEX `fk_Endereco_Cidade1_idx` (`idCidade` ASC),
  CONSTRAINT `fk_Endereco_Pessoa1`
    FOREIGN KEY (`idPessoa`)
    REFERENCES `aps`.`Pessoa` (`idPessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Endereco_Cidade1`
    FOREIGN KEY (`idCidade`)
    REFERENCES `aps`.`Cidade` (`idCidade`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Frete`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Frete` (
  `idFrete` INT NOT NULL AUTO_INCREMENT,
  `idPessoaRemetente` INT NOT NULL,
  `idPessoaDestinatario` INT NOT NULL,
  `idPessoaTomador` INT NOT NULL,
  `idPessoaMotorista` INT NOT NULL,
  `NumeroCTe` INT NOT NULL,
  `DataEmissao` DATE NOT NULL,
  `DataEntrega` DATE NULL,
  `VlCarga` DOUBLE NULL,
  `VlPedagio` DOUBLE NULL,
  `VlFrete` DOUBLE NOT NULL,
  `PesoBruto` DOUBLE NULL,
  `Finalizado` TINYINT(1) NOT NULL,
  PRIMARY KEY (`idFrete`),
  INDEX `fk_Frete_Pessoa1_idx` (`idPessoaRemetente` ASC),
  INDEX `fk_Frete_Pessoa3_idx` (`idPessoaTomador` ASC),
  INDEX `fk_Frete_Pessoa4_idx` (`idPessoaMotorista` ASC),
  INDEX `fk_Frete_Pessoa2_idx` (`idPessoaDestinatario` ASC),
  CONSTRAINT `fk_Frete_Pessoa1`
    FOREIGN KEY (`idPessoaRemetente`)
    REFERENCES `aps`.`Pessoa` (`idPessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Frete_Pessoa3`
    FOREIGN KEY (`idPessoaTomador`)
    REFERENCES `aps`.`Pessoa` (`idPessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Frete_Pessoa4`
    FOREIGN KEY (`idPessoaMotorista`)
    REFERENCES `aps`.`Pessoa` (`idPessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Frete_Pessoa2`
    FOREIGN KEY (`idPessoaDestinatario`)
    REFERENCES `aps`.`Pessoa` (`idPessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Marca`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Marca` (
  `idMarca` INT NOT NULL AUTO_INCREMENT,
  `Descricao` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idMarca`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`TipoVeiculo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`TipoVeiculo` (
  `idTipoVeiculo` INT NOT NULL AUTO_INCREMENT,
  `Descricao` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idTipoVeiculo`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Modelo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Modelo` (
  `idModelo` INT NOT NULL AUTO_INCREMENT,
  `idMarca` INT NOT NULL,
  `idTipoVeiculo` INT NOT NULL,
  `Descricao` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idModelo`),
  INDEX `fk_Modelo_Marca1_idx` (`idMarca` ASC),
  INDEX `fk_Modelo_TipoVeiculo1_idx` (`idTipoVeiculo` ASC),
  CONSTRAINT `fk_Modelo_Marca1`
    FOREIGN KEY (`idMarca`)
    REFERENCES `aps`.`Marca` (`idMarca`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Modelo_TipoVeiculo1`
    FOREIGN KEY (`idTipoVeiculo`)
    REFERENCES `aps`.`TipoVeiculo` (`idTipoVeiculo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Veiculo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Veiculo` (
  `idVeiculo` INT NOT NULL AUTO_INCREMENT,
  `idModelo` INT NOT NULL,
  `Descricao` VARCHAR(45) NOT NULL,
  `Renavam` VARCHAR(45) NOT NULL,
  `Placa` VARCHAR(45) NOT NULL,
  `Tara` VARCHAR(45) NOT NULL,
  `CapacidadeKg` VARCHAR(45) NULL,
  `CapacidadeM3` VARCHAR(45) NULL,
  PRIMARY KEY (`idVeiculo`),
  INDEX `fk_Veiculo_Modelo1_idx` (`idModelo` ASC),
  CONSTRAINT `fk_Veiculo_Modelo1`
    FOREIGN KEY (`idModelo`)
    REFERENCES `aps`.`Modelo` (`idModelo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Telefone`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Telefone` (
  `idTelefone` INT NOT NULL AUTO_INCREMENT,
  `idPessoa` INT NOT NULL,
  `Telefone` VARCHAR(15) NOT NULL,
  PRIMARY KEY (`idTelefone`),
  INDEX `fk_Telefone_Pessoa1_idx` (`idPessoa` ASC),
  CONSTRAINT `fk_Telefone_Pessoa1`
    FOREIGN KEY (`idPessoa`)
    REFERENCES `aps`.`Pessoa` (`idPessoa`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `aps`.`Frete_Veiculo`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `aps`.`Frete_Veiculo` (
  `idFrete` INT NOT NULL,
  `idVeiculo` INT NOT NULL,
  PRIMARY KEY (`idFrete`, `idVeiculo`),
  INDEX `fk_Frete_has_Veiculo_Veiculo1_idx` (`idVeiculo` ASC),
  INDEX `fk_Frete_has_Veiculo_Frete1_idx` (`idFrete` ASC),
  CONSTRAINT `fk_Frete_has_Veiculo_Frete1`
    FOREIGN KEY (`idFrete`)
    REFERENCES `aps`.`Frete` (`idFrete`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Frete_has_Veiculo_Veiculo1`
    FOREIGN KEY (`idVeiculo`)
    REFERENCES `aps`.`Veiculo` (`idVeiculo`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
