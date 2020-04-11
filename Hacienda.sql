USE [master]
GO
/****** Object:  Database [Hacienda]    Script Date: 4/10/2020 8:49:41 PM ******/
CREATE DATABASE [Hacienda]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Hacienda', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Hacienda.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Hacienda_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\DATA\Hacienda_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Hacienda] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Hacienda].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Hacienda] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Hacienda] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Hacienda] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Hacienda] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Hacienda] SET ARITHABORT OFF 
GO
ALTER DATABASE [Hacienda] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Hacienda] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Hacienda] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Hacienda] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Hacienda] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Hacienda] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Hacienda] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Hacienda] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Hacienda] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Hacienda] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Hacienda] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Hacienda] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Hacienda] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Hacienda] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Hacienda] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Hacienda] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Hacienda] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Hacienda] SET RECOVERY FULL 
GO
ALTER DATABASE [Hacienda] SET  MULTI_USER 
GO
ALTER DATABASE [Hacienda] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Hacienda] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Hacienda] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Hacienda] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Hacienda] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Hacienda', N'ON'
GO
ALTER DATABASE [Hacienda] SET QUERY_STORE = OFF
GO
USE [Hacienda]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [Hacienda]
GO
/****** Object:  Table [dbo].[DetalleProduccion]    Script Date: 4/10/2020 8:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DetalleProduccion](
	[IdDetalleProduccion] [int] IDENTITY(1,1) NOT NULL,
	[IdProduccion] [int] NULL,
	[Cantidad] [float] NOT NULL,
	[PrecioVenta] [float] NOT NULL,
	[FechaProduccion] [date] NULL,
	[TotalVenta]  AS ([Cantidad]*[PrecioVenta]),
PRIMARY KEY CLUSTERED 
(
	[IdDetalleProduccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Empleados]    Script Date: 4/10/2020 8:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Empleados](
	[IdEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[Cedula] [varchar](11) NOT NULL,
	[Nombre] [varchar](32) NOT NULL,
	[Apellidos] [varchar](60) NOT NULL,
	[Sexo] [varchar](1) NOT NULL,
	[Telefono] [varchar](12) NOT NULL,
	[Email] [varchar](45) NULL,
	[Direccion] [varchar](255) NULL,
	[Puesto] [int] NOT NULL,
	[FechaEntrada] [date] NOT NULL,
	[FechaActualizacion] [date] NULL,
	[Estado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[Cedula] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadoEmpleado]    Script Date: 4/10/2020 8:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadoEmpleado](
	[IdStausEmpleado] [int] IDENTITY(1,1) NOT NULL,
	[Estado] [varchar](36) NOT NULL,
	[Detalles] [varchar](82) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdStausEmpleado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MisProductos]    Script Date: 4/10/2020 8:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MisProductos](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Producto] [varchar](32) NOT NULL,
	[Descripcion] [varchar](122) NULL,
	[Estado] [int] NOT NULL,
	[FechaCreacion] [date] NOT NULL,
	[FechaActualizacion] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Produccion]    Script Date: 4/10/2020 8:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Produccion](
	[IdProduccion] [int] IDENTITY(1,1) NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Unidad] [varchar](16) NOT NULL,
	[Turno] [varchar](10) NOT NULL,
	[EstadoFacturacion] [int] NOT NULL,
	[Proveedor] [int] NULL,
	[Despachado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProduccion] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 4/10/2020 8:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[IdProveedor] [int] IDENTITY(1,1) NOT NULL,
	[TipoProveedor] [varchar](44) NOT NULL,
	[TipoDocumento] [varchar](10) NOT NULL,
	[NumDocumento] [varchar](18) NULL,
	[NombreProveedor] [varchar](34) NOT NULL,
	[SegundoNombre] [varchar](60) NULL,
	[Celular] [varchar](13) NOT NULL,
	[Telefono] [varchar](13) NULL,
	[Email] [varchar](46) NOT NULL,
	[Direccion] [varchar](128) NOT NULL,
	[FechaCreacion] [date] NOT NULL,
	[FechaActualizacion] [date] NOT NULL,
	[Estado] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
UNIQUE NONCLUSTERED 
(
	[NumDocumento] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Puestos]    Script Date: 4/10/2020 8:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Puestos](
	[IdPuesto] [int] IDENTITY(1,1) NOT NULL,
	[NomPuesto] [varchar](32) NOT NULL,
	[Descripcion] [varchar](132) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPuesto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 4/10/2020 8:49:42 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Usuario] [varchar](12) NOT NULL,
	[Clave] [varchar](255) NULL,
	[Permisos] [varchar](16) NOT NULL,
	[CodEmpleado] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[DetalleProduccion] ADD  DEFAULT (getdate()) FOR [FechaProduccion]
GO
ALTER TABLE [dbo].[Empleados] ADD  DEFAULT (getdate()) FOR [FechaActualizacion]
GO
ALTER TABLE [dbo].[MisProductos] ADD  DEFAULT (getdate()) FOR [FechaActualizacion]
GO
ALTER TABLE [dbo].[Proveedores] ADD  DEFAULT (getdate()) FOR [FechaActualizacion]
GO
ALTER TABLE [dbo].[Usuarios] ADD  DEFAULT ('USER') FOR [Permisos]
GO
ALTER TABLE [dbo].[DetalleProduccion]  WITH CHECK ADD FOREIGN KEY([IdProduccion])
REFERENCES [dbo].[Produccion] ([IdProduccion])
GO
ALTER TABLE [dbo].[Empleados]  WITH CHECK ADD FOREIGN KEY([Puesto])
REFERENCES [dbo].[Puestos] ([IdPuesto])
GO
ALTER TABLE [dbo].[Empleados]  WITH CHECK ADD  CONSTRAINT [FK_EmpEstado] FOREIGN KEY([Estado])
REFERENCES [dbo].[EstadoEmpleado] ([IdStausEmpleado])
GO
ALTER TABLE [dbo].[Empleados] CHECK CONSTRAINT [FK_EmpEstado]
GO
ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD FOREIGN KEY([Despachado])
REFERENCES [dbo].[Empleados] ([IdEmpleado])
GO
ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD FOREIGN KEY([IdProducto])
REFERENCES [dbo].[MisProductos] ([IdProducto])
GO
ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD FOREIGN KEY([Proveedor])
REFERENCES [dbo].[Proveedores] ([IdProveedor])
GO
ALTER TABLE [dbo].[MisProductos]  WITH CHECK ADD CHECK  (([Estado]=(0) OR [Estado]=(1)))
GO
ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD CHECK  (([Turno]='Tarde' OR [Turno]='Madrugada'))
GO
ALTER TABLE [dbo].[Produccion]  WITH CHECK ADD CHECK  (([Unidad]='Galones' OR [Unidad]='Botellas' OR [Unidad]='Litros'))
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD CHECK  (([TipoDocumento]='Pasaporte' OR [TipoDocumento]='RNC' OR [TipoDocumento]='Cedula'))
GO
ALTER TABLE [dbo].[Proveedores]  WITH CHECK ADD CHECK  (([TipoProveedor]='Persona' OR [TipoProveedor]='Entidad'))
GO
USE [master]
GO
ALTER DATABASE [Hacienda] SET  READ_WRITE 
GO
