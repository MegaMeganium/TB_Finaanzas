-- Created by Vertabelo (http://vertabelo.com)
-- Last modification date: 2018-06-06 17:32:21.374

-- tables
-- Table: Bono
CREATE TABLE Bono (
    BonoID bigint  NOT NULL IDENTITY,
    Entidad_ID nvarchar(30)  NOT NULL,
    ValorNominal money  NOT NULL,
    ValorComercial int  NOT NULL,
    NroAnios int  NOT NULL,
    FrecCupon int  NOT NULL,
    DiasPorAnio int  NOT NULL,
    ImpRenta int  NOT NULL,
    FechaEmision date  NOT NULL,
    TasaAnualDescuento int  NOT NULL,
    CONSTRAINT Bono_pk PRIMARY KEY  (BonoID)
);

-- Table: Bono_Tasa
CREATE TABLE Bono_Tasa (
    ID int  IDENTITY(1,1) NOT NULL,
    TipoTasa_ID int  NOT NULL,
    Bono_BonoID bigint  NOT NULL,
    TasaInteres int  NOT NULL,
    NroCuota int  NOT NULL,
    capitalizacion int  NULL,
    CONSTRAINT Bono_Tasa_pk PRIMARY KEY  (ID)
);

-- Table: Capitalizacion
CREATE TABLE Capitalizacion (
    ID int  NOT NULL IDENTITY,
    Nombre nvarchar(15)  NOT NULL,
    CONSTRAINT Capitalizacion_pk PRIMARY KEY  (ID)
);

-- Table: Costes_Gastos
CREATE TABLE Costes_Gastos (
    ID int  NOT NULL IDENTITY,
    Bono_ID bigint  NOT NULL,
    Nombre nvarchar(15)  NOT NULL,
    Valor int  NOT NULL,
    Inicial bit  NOT NULL,
    Emisor bit  NOT NULL,
    Receptor bit  NOT NULL,
    CONSTRAINT Costes_Gastos_pk PRIMARY KEY  (ID)
);

-- Table: Entidad
CREATE TABLE Entidad (
    ID_email nvarchar(30)  NOT NULL,
    Password nvarchar(8)  NOT NULL,
    Nombre nvarchar(50)  NOT NULL,
    Apellido nvarchar(50)  NOT NULL,
    CONSTRAINT Entidad_pk PRIMARY KEY  (ID_email)
);

-- Table: Inflacion
CREATE TABLE Inflacion (
    ID int  NOT NULL IDENTITY,
    Bono_ID bigint  NOT NULL,
    Fecha date  NOT NULL,
    CONSTRAINT Inflacion_pk PRIMARY KEY  (ID)
);

-- Table: PlazoBono
CREATE TABLE PlazoBono (
    ID int  NOT NULL IDENTITY,
    Bono_ID bigint  NOT NULL,
    PlazoGracia_ID int  NOT NULL,
    Fecha date  NOT NULL,
    CONSTRAINT PlazoBono_pk PRIMARY KEY  (ID)
);

-- Table: PlazoGracia
CREATE TABLE PlazoGracia (
    ID int  NOT NULL IDENTITY,
    Nombre nvarchar(15)  NOT NULL,
    Descripcion text  NULL,
    CONSTRAINT PlazoGracia_pk PRIMARY KEY  (ID)
);

-- Table: TipoTasa
CREATE TABLE TipoTasa (
    ID int  NOT NULL IDENTITY,
    Nombre nvarchar(15)  NOT NULL,
    CONSTRAINT TipoTasa_pk PRIMARY KEY  (ID)
);

-- foreign keys
-- Reference: Bono_Entidad (table: Bono)
ALTER TABLE Bono ADD CONSTRAINT Bono_Entidad
    FOREIGN KEY (Entidad_ID)
    REFERENCES Entidad (ID_email);

-- Reference: Bono_Tasa_Bono (table: Bono_Tasa)
ALTER TABLE Bono_Tasa ADD CONSTRAINT Bono_Tasa_Bono
    FOREIGN KEY (Bono_BonoID)
    REFERENCES Bono (BonoID);

-- Reference: Bono_Tasa_Capitalizacion (table: Bono_Tasa)
ALTER TABLE Bono_Tasa ADD CONSTRAINT Bono_Tasa_Capitalizacion
    FOREIGN KEY (capitalizacion)
    REFERENCES Capitalizacion (ID);

-- Reference: Bono_Tasa_TipoTasa (table: Bono_Tasa)
ALTER TABLE Bono_Tasa ADD CONSTRAINT Bono_Tasa_TipoTasa
    FOREIGN KEY (TipoTasa_ID)
    REFERENCES TipoTasa (ID);

-- Reference: Costes_Gastos_Bono (table: Costes_Gastos)
ALTER TABLE Costes_Gastos ADD CONSTRAINT Costes_Gastos_Bono
    FOREIGN KEY (Bono_ID)
    REFERENCES Bono (BonoID);

-- Reference: Inflacion_Bono (table: Inflacion)
ALTER TABLE Inflacion ADD CONSTRAINT Inflacion_Bono
    FOREIGN KEY (Bono_ID)
    REFERENCES Bono (BonoID);

-- Reference: PlazoBono_Bono (table: PlazoBono)
ALTER TABLE PlazoBono ADD CONSTRAINT PlazoBono_Bono
    FOREIGN KEY (Bono_ID)
    REFERENCES Bono (BonoID);

-- Reference: PlazoBono_PlazoGracia (table: PlazoBono)
ALTER TABLE PlazoBono ADD CONSTRAINT PlazoBono_PlazoGracia
    FOREIGN KEY (PlazoGracia_ID)
    REFERENCES PlazoGracia (ID);

-- End of file.

