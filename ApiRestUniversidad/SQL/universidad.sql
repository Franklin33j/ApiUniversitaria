-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 03-12-2022 a las 19:10:39
-- Versión del servidor: 10.4.21-MariaDB
-- Versión de PHP: 7.4.25

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `universidad`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `careers`
--

CREATE TABLE `careers` (
  `idCareer` int(11) NOT NULL,
  `nameCareer` varchar(255) NOT NULL,
  `idFaculty` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `careers`
--

INSERT INTO `careers` (`idCareer`, `nameCareer`, `idFaculty`) VALUES
(1, 'Ingenieria Informatica', 1),
(2, 'LICENCIATURA EN ESTUDIOS SOCIALES', 5),
(3, 'string', 1);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `faculties`
--

CREATE TABLE `faculties` (
  `idFaculty` int(11) NOT NULL,
  `nameFaculty` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `faculties`
--

INSERT INTO `faculties` (`idFaculty`, `nameFaculty`) VALUES
(8, 'FACULTA DE CIENCIAS ECONOMICAS22'),
(9, 'FACULTAD DE CIENCIAS ECONOMICAS'),
(5, 'FACULTAD DE CIENCIAS SOCIALES'),
(1, 'FACULTAD DE DERECHO');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `ratings`
--

CREATE TABLE `ratings` (
  `IdRating` int(11) NOT NULL,
  `note1` decimal(10,0) NOT NULL,
  `note2` decimal(10,0) NOT NULL,
  `note3` decimal(10,0) NOT NULL,
  `note4` decimal(10,0) NOT NULL,
  `idStudent` int(11) NOT NULL,
  `idSubject` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `ratings`
--

INSERT INTO `ratings` (`IdRating`, `note1`, `note2`, `note3`, `note4`, `idStudent`, `idSubject`) VALUES
(1, '0', '0', '0', '0', 1, 1),
(2, '3', '5', '7', '0', 4, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `recordinscriptionstudents`
--

CREATE TABLE `recordinscriptionstudents` (
  `idRecord` int(11) NOT NULL,
  `idStudent` int(11) NOT NULL,
  `idSubject` int(11) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `recordinscriptionteachers`
--

CREATE TABLE `recordinscriptionteachers` (
  `idRecord` int(11) NOT NULL,
  `idSubject` int(11) NOT NULL,
  `idTeacher` int(11) NOT NULL,
  `date` datetime NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `students`
--

CREATE TABLE `students` (
  `idStudent` int(11) NOT NULL,
  `firstNames` varchar(255) NOT NULL,
  `lastNames` varchar(255) NOT NULL,
  `idCareer` int(11) DEFAULT NULL,
  `cum` float NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `students`
--

INSERT INTO `students` (`idStudent`, `firstNames`, `lastNames`, `idCareer`, `cum`) VALUES
(1, 'JOSE FRANKLIN', 'ANGEL GUEVARA', 1, 10),
(2, 'JOSE FRANKLIN', 'ANGEL GUEVARA', 1, 10),
(3, 'JOSE FRANKLIN', 'ANGEL GUEVARA', 1, 10),
(4, 'LUIS ANTONIO', 'DIAZ MOZ', 2, 7);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `subjects`
--

CREATE TABLE `subjects` (
  `idSubject` int(11) NOT NULL,
  `nameSubject` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Volcado de datos para la tabla `subjects`
--

INSERT INTO `subjects` (`idSubject`, `nameSubject`) VALUES
(1, 'FISICA PARA INGENIERIA'),
(2, 'MATEMATICAS I');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `teachers`
--

CREATE TABLE `teachers` (
  `IdTeacher` int(255) NOT NULL,
  `firstNames` varchar(255) NOT NULL,
  `lastNames` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `careers`
--
ALTER TABLE `careers`
  ADD PRIMARY KEY (`idCareer`),
  ADD KEY `Careers_fk0` (`idFaculty`);

--
-- Indices de la tabla `faculties`
--
ALTER TABLE `faculties`
  ADD PRIMARY KEY (`idFaculty`),
  ADD UNIQUE KEY `nameFaculty` (`nameFaculty`);

--
-- Indices de la tabla `ratings`
--
ALTER TABLE `ratings`
  ADD PRIMARY KEY (`IdRating`),
  ADD KEY `Ratings_fk0` (`idStudent`),
  ADD KEY `Ratings_fk1` (`idSubject`);

--
-- Indices de la tabla `recordinscriptionstudents`
--
ALTER TABLE `recordinscriptionstudents`
  ADD PRIMARY KEY (`idRecord`),
  ADD KEY `recordInscriptionStudents_fk0` (`idStudent`),
  ADD KEY `recordInscriptionStudents_fk1` (`idSubject`);

--
-- Indices de la tabla `recordinscriptionteachers`
--
ALTER TABLE `recordinscriptionteachers`
  ADD PRIMARY KEY (`idRecord`),
  ADD KEY `recordInscriptionTeachers_fk0` (`idSubject`),
  ADD KEY `recordInscriptionTeachers_fk1` (`idTeacher`);

--
-- Indices de la tabla `students`
--
ALTER TABLE `students`
  ADD PRIMARY KEY (`idStudent`),
  ADD KEY `Students_fk0` (`idCareer`);

--
-- Indices de la tabla `subjects`
--
ALTER TABLE `subjects`
  ADD PRIMARY KEY (`idSubject`);

--
-- Indices de la tabla `teachers`
--
ALTER TABLE `teachers`
  ADD PRIMARY KEY (`IdTeacher`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `careers`
--
ALTER TABLE `careers`
  MODIFY `idCareer` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT de la tabla `faculties`
--
ALTER TABLE `faculties`
  MODIFY `idFaculty` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=10;

--
-- AUTO_INCREMENT de la tabla `ratings`
--
ALTER TABLE `ratings`
  MODIFY `IdRating` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `recordinscriptionstudents`
--
ALTER TABLE `recordinscriptionstudents`
  MODIFY `idRecord` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `recordinscriptionteachers`
--
ALTER TABLE `recordinscriptionteachers`
  MODIFY `idRecord` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de la tabla `students`
--
ALTER TABLE `students`
  MODIFY `idStudent` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `subjects`
--
ALTER TABLE `subjects`
  MODIFY `idSubject` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `teachers`
--
ALTER TABLE `teachers`
  MODIFY `IdTeacher` int(255) NOT NULL AUTO_INCREMENT;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `careers`
--
ALTER TABLE `careers`
  ADD CONSTRAINT `Careers_fk0` FOREIGN KEY (`idFaculty`) REFERENCES `faculties` (`idFaculty`);

--
-- Filtros para la tabla `ratings`
--
ALTER TABLE `ratings`
  ADD CONSTRAINT `Ratings_fk0` FOREIGN KEY (`idStudent`) REFERENCES `students` (`idStudent`),
  ADD CONSTRAINT `Ratings_fk1` FOREIGN KEY (`idSubject`) REFERENCES `subjects` (`idSubject`);

--
-- Filtros para la tabla `recordinscriptionstudents`
--
ALTER TABLE `recordinscriptionstudents`
  ADD CONSTRAINT `recordInscriptionStudents_fk0` FOREIGN KEY (`idStudent`) REFERENCES `students` (`idStudent`),
  ADD CONSTRAINT `recordInscriptionStudents_fk1` FOREIGN KEY (`idSubject`) REFERENCES `subjects` (`idSubject`);

--
-- Filtros para la tabla `recordinscriptionteachers`
--
ALTER TABLE `recordinscriptionteachers`
  ADD CONSTRAINT `recordInscriptionTeachers_fk0` FOREIGN KEY (`idSubject`) REFERENCES `subjects` (`idSubject`),
  ADD CONSTRAINT `recordInscriptionTeachers_fk1` FOREIGN KEY (`idTeacher`) REFERENCES `teachers` (`IdTeacher`);

--
-- Filtros para la tabla `students`
--
ALTER TABLE `students`
  ADD CONSTRAINT `Students_fk0` FOREIGN KEY (`idCareer`) REFERENCES `careers` (`idCareer`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
