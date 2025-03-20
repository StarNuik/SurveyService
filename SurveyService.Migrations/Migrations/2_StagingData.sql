--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4 (Debian 17.4-1.pgdg120+2)
-- Dumped by pg_dump version 17.4 (Debian 17.4-1.pgdg120+2)

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- Data for Name: VersionInfo; Type: TABLE DATA; Schema: public; Owner: postgres
--

-- INSERT INTO public."VersionInfo" VALUES (1, '2025-03-19 18:07:51', 'AddTables');


--
-- Data for Name: survey; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.survey OVERRIDING SYSTEM VALUE VALUES (17, 'какой ты кот', '{14, 15, 16}');
INSERT INTO public.survey OVERRIDING SYSTEM VALUE VALUES (18, 'какой ты собак', '{20, 21, 22}');


--
-- Data for Name: question; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.question OVERRIDING SYSTEM VALUE VALUES (14, 'какой у тебя размер?', 17);
INSERT INTO public.question OVERRIDING SYSTEM VALUE VALUES (15, 'какой у тебя шерсть?', 17);
INSERT INTO public.question OVERRIDING SYSTEM VALUE VALUES (16, 'какой у тебя лапы?', 17);
INSERT INTO public.question OVERRIDING SYSTEM VALUE VALUES (20, 'какой у тебя цвет?', 18);
INSERT INTO public.question OVERRIDING SYSTEM VALUE VALUES (21, 'какой у тебя шерсть?', 18);
INSERT INTO public.question OVERRIDING SYSTEM VALUE VALUES (22, 'какой у тебя нос?', 18);


--
-- Data for Name: answer; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (27, 'как бочонок', 14);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (28, 'как подушка', 14);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (29, 'как кружка', 14);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (30, 'длиннющая', 15);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (31, 'как у всех', 15);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (32, 'без шерсти', 15);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (33, 'мягкие', 16);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (34, 'очень мягкие', 16);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (35, 'невероятно мягкие', 16);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (36, 'черный', 20);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (37, 'белый', 20);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (38, 'золотой', 20);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (39, 'мягкий', 21);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (40, 'короткий', 21);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (41, 'жесткий', 21);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (42, 'мокрый', 22);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (43, 'очень мокрый', 22);
INSERT INTO public.answer OVERRIDING SYSTEM VALUE VALUES (44, 'сухой(', 22);


--
-- Data for Name: interview; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Data for Name: result; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- Name: answer_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.answer_id_seq', 44, true);


--
-- Name: interview_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.interview_id_seq', 9, true);


--
-- Name: question_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.question_id_seq', 22, true);


--
-- Name: result_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.result_id_seq', 9, true);


--
-- Name: survey_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.survey_id_seq', 18, true);


--
-- PostgreSQL database dump complete
--

