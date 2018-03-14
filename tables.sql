
CREATE TABLE `users` (
  `id` int(11) NOT NULL,
  `mail` varchar(255) NOT NULL,
  `password` varchar(255) NOT NULL,
  `type` int(11) NOT NULL DEFAULT '0',
  `ulice` varchar(255) NOT NULL,
  `psc` varchar(255) NOT NULL,
  `cp` varchar(255) NOT NULL
);