 IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'SatelliteDb')
	BEGIN
		CREATE DATABASE [SatelliteDb]
	END
GO

USE [SatelliteDb]
GO

BEGIN
	IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='SAT' and xtype='U')
		CREATE TABLE SAT (
			catelog_number INT NOT NULL PRIMARY KEY,
			classification CHAR,
			launch_year INT,
			launch_num_and_designator NVARCHAR(10)
		);
	IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='LOC' and xtype='U')
		CREATE TABLE LOC (
			loc_id INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
			sat_id INT NOT NULL FOREIGN KEY REFERENCES SAT(catelog_number) ON DELETE CASCADE,
			date DATETIME,
			first_deriv_mean FLOAT,
			second_deriv_mean FLOAT,
			drag_term FLOAT,
			elem_set_number INT,
			inclination FLOAT,
			right_asc FLOAT,
			eccentricity FLOAT,
			arg_perigree FLOAT,
			mean_anomaly FLOAT,
			mean_motion FLOAT,
			rev_number_at_epoch INT
		);
END
GO