-- Active: 1717034392849@@bcpuwyy7nejwbckj2l7r-mysql.services.clever-cloud.com@3306
CREATE TABLE Especialidades(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombre VARCHAR(125),
    Descripcion TEXT,
    Estado ENUM('Activo','Inactivo')
);

SELECT * FROM Especialidades;

CREATE TABLE Pacientes(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    Nombres VARCHAR(125),
    Apellidos VARCHAR(125),
    FechaNacimiento DATE,
    Correo VARCHAR(125) UNIQUE,
    Telefono VARCHAR(75),
    Direccion VARCHAR(125),
    Estado ENUM('Activo','Inactivo')
);

CREATE TABLE Medicos(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NombreCompleto VARCHAR(125),
    EspecialidadId INT,
    Correo VARCHAR(125) UNIQUE,
    Telefono VARCHAR(75),
    Estado ENUM('Activo','Inactivo')
);

CREATE TABLE Citas(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    MedicoId INT,
    PacienteId INT,
    Fecha DATE,
    Estado ENUM('Activo','Inactivo')
);

CREATE TABLE Tratamientos(
    Id INT PRIMARY KEY AUTO_INCREMENT,
    CitaId INT,
    Description TEXT,
    Estado ENUM('Activo','Inactivo')
);


ALTER TABLE Medicos ADD FOREIGN KEY (EspecialidadId) REFERENCES Especialidades(Id);
ALTER TABLE Citas ADD FOREIGN KEY (MedicoId) REFERENCES Medicos(Id);
ALTER TABLE Citas ADD FOREIGN KEY (PacienteId) REFERENCES Pacientes(Id);
ALTER TABLE Tratamientos ADD FOREIGN KEY (CitaId) REFERENCES Citas(Id);

INSERT INTO Especialidades(Nombre, Descripcion, Estado) VALUES
('Cardiología', 'Especialidad médica dedicada al diagnóstico y tratamiento de enfermedades del corazón.', 'Activo'),
('Neurología', 'Rama de la medicina que trata los trastornos del sistema nervioso.', 'Activo'),
('Dermatología', 'Especialidad médica que se ocupa del diagnóstico y tratamiento de las enfermedades de la piel.', 'Inactivo'),
('Pediatría', 'Rama de la medicina que se ocupa de la salud y enfermedades de los niños.', 'Activo'),
('Ginecología', 'Especialidad médica que trata las enfermedades del sistema reproductor femenino.', 'Activo'),
('Psiquiatría', 'Rama de la medicina que se dedica al estudio y tratamiento de las enfermedades mentales.', 'Inactivo'),
('Ortopedia', 'Especialidad médica que se ocupa de corregir o evitar las deformidades del sistema músculo-esquelético.', 'Activo');


INSERT INTO Pacientes (Nombres, Apellidos, FechaNacimiento, Correo, Telefono, Direccion, Estado) VALUES
('Juan', 'Perez', '1985-06-15', 'juan.perez@example.com', '555-1234', '123 Calle Principal', 'Activo'),
('Maria', 'Gonzalez', '1990-07-22', 'maria.gonzalez@example.com', '555-5678', '456 Avenida Secundaria', 'Inactivo'),
('Carlos', 'Lopez', '1978-09-10', 'carlos.lopez@example.com', '555-8765', '789 Calle Tercera', 'Activo'),
('Ana', 'Martinez', '1983-12-05', 'ana.martinez@example.com', '555-4321', '321 Calle Cuarta', 'Inactivo'),
('Luis', 'Rodriguez', '1992-03-18', 'luis.rodriguez@example.com', '555-6789', '654 Calle Quinta', 'Activo'),
('Laura', 'Hernandez', '1987-11-30', 'laura.hernandez@example.com', '555-9876', '987 Calle Sexta', 'Inactivo'),
('Jose', 'Garcia', '1995-04-25', 'jose.garcia@example.com', '555-5432', '111 Calle Septima', 'Activo'),
('Elena', 'Sanchez', '1989-08-20', 'elena.sanchez@example.com', '555-6543', '222 Calle Octava', 'Inactivo'),
('David', 'Ramirez', '1984-02-11', 'david.ramirez@example.com', '555-7890', '333 Calle Novena', 'Activo'),
('Isabel', 'Fernandez', '1991-05-14', 'isabel.fernandez@example.com', '555-8901', '444 Calle Decima', 'Inactivo');

INSERT INTO Medicos (NombreCompleto, EspecialidadId, Correo, Telefono, Estado) VALUES
('Dr. Juan Perez', 1, 'juan.perez@hospital.com', '555-1234', 'Activo'),
('Dra. Maria Gonzalez', 2, 'maria.gonzalez@hospital.com', '555-5678', 'Inactivo'),
('Dr. Carlos Lopez', 3, 'carlos.lopez@hospital.com', '555-8765', 'Activo'),
('Dra. Ana Martinez', 4, 'ana.martinez@hospital.com', '555-4321', 'Inactivo'),
('Dr. Luis Rodriguez', 5, 'luis.rodriguez@hospital.com', '555-6789', 'Activo'),
('Dra. Laura Hernandez', 6, 'laura.hernandez@hospital.com', '555-9876', 'Inactivo'),
('Dr. Jose Garcia', 7, 'jose.garcia@hospital.com', '555-5432', 'Activo'),
('Dra. Elena Sanchez', 1, 'elena.sanchez@hospital.com', '555-6543', 'Inactivo'),
('Dr. David Ramirez', 2, 'david.ramirez@hospital.com', '555-7890', 'Activo'),
('Dra. Isabel Fernandez', 3, 'isabel.fernandez@hospital.com', '555-8901', 'Inactivo');

INSERT INTO Citas (MedicoId, PacienteId, Fecha, Estado) VALUES
(1, 1, '2024-06-01 10:00:00', 'Activo'),
(2, 2, '2024-06-02 11:00:00', 'Inactivo'),
(3, 3, '2024-06-03 09:00:00', 'Activo'),
(4, 4, '2024-06-04 08:30:00', 'Inactivo'),
(5, 5, '2024-06-05 13:00:00', 'Activo'),
(6, 6, '2024-06-06 14:00:00', 'Inactivo'),
(7, 7, '2024-06-07 15:00:00', 'Activo'),
(8, 8, '2024-06-08 10:30:00', 'Inactivo'),
(9, 9, '2024-06-09 11:30:00', 'Activo'),
(10, 10, '2024-06-10 12:00:00', 'Inactivo');


INSERT INTO Tratamientos (CitaId, Description, Estado) VALUES
(1, 'Tratamiento de fisioterapia para dolor de espalda', 'Activo'),
(2, 'Terapia ocupacional post-operatoria', 'Inactivo'),
(3, 'Sesión de quimioterapia', 'Activo'),
(4, 'Tratamiento de rehabilitación cardíaca', 'Inactivo'),
(5, 'Terapia de manejo del dolor crónico', 'Activo'),
(6, 'Rehabilitación post-accidente cerebrovascular', 'Inactivo'),
(7, 'Terapia de lenguaje y habla', 'Activo'),
(8, 'Tratamiento de rehabilitación ortopédica', 'Inactivo'),
(9, 'Sesión de radioterapia', 'Activo'),
(10, 'Tratamiento de desintoxicación de drogas', 'Inactivo');

