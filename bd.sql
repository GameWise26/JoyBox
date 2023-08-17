PGDMP     5                     {             Joybox    10.23    10.23 7    T           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                       false            U           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                       false            V           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                       false            W           1262    16393     Joybox    DATABASE     �   CREATE DATABASE " Joybox" WITH TEMPLATE = template0 ENCODING = 'UTF8' LC_COLLATE = 'Spanish_Argentina.1252' LC_CTYPE = 'Spanish_Argentina.1252';
    DROP DATABASE " Joybox";
             postgres    false            X           0    0    DATABASE " Joybox"    COMMENT     4   COMMENT ON DATABASE " Joybox" IS 'Tablas tablitas';
                  postgres    false    2903                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
             postgres    false            Y           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                  postgres    false    3                        3079    12924    plpgsql 	   EXTENSION     ?   CREATE EXTENSION IF NOT EXISTS plpgsql WITH SCHEMA pg_catalog;
    DROP EXTENSION plpgsql;
                  false            Z           0    0    EXTENSION plpgsql    COMMENT     @   COMMENT ON EXTENSION plpgsql IS 'PL/pgSQL procedural language';
                       false    1            _           1247    16424    actividadusu    TYPE     J   CREATE TYPE public.actividadusu AS ENUM (
    'activo',
    'inactivo'
);
    DROP TYPE public.actividadusu;
       public       postgres    false    3            }           1247    16475    estadoPartida    TYPE     a   CREATE TYPE public."estadoPartida" AS ENUM (
    'en espera',
    'en curso',
    'terminado'
);
 "   DROP TYPE public."estadoPartida";
       public       postgres    false    3                       1247    16416 
   estadosoli    TYPE     \   CREATE TYPE public.estadosoli AS ENUM (
    'aceptado',
    'rechazado',
    'en espera'
);
    DROP TYPE public.estadosoli;
       public       postgres    false    3            q           1247    16459    rolGrupo    TYPE     F   CREATE TYPE public."rolGrupo" AS ENUM (
    'admin',
    'miembro'
);
    DROP TYPE public."rolGrupo";
       public       postgres    false    3            \           1247    16400 	   tiponotif    TYPE     G   CREATE TYPE public.tiponotif AS ENUM (
    'aviso',
    'solicitud'
);
    DROP TYPE public.tiponotif;
       public       postgres    false    3            �            1259    16435 	   actividad    TABLE     o   CREATE TABLE public.actividad (
    usuario_id integer NOT NULL,
    actividad public.actividadusu NOT NULL
);
    DROP TABLE public.actividad;
       public         postgres    false    607    3            �            1259    16443    amigos    TABLE     �   CREATE TABLE public.amigos (
    id integer NOT NULL,
    usu1_id integer NOT NULL,
    usu2_id integer NOT NULL,
    fecha_inicio date NOT NULL
);
    DROP TABLE public.amigos;
       public         postgres    false    3            �            1259    16448    chats    TABLE     �   CREATE TABLE public.chats (
    id integer NOT NULL,
    usu1_id integer NOT NULL,
    usu2_id integer NOT NULL,
    fecha_inicio date NOT NULL
);
    DROP TABLE public.chats;
       public         postgres    false    3            �            1259    16466    comunidades    TABLE     �   CREATE TABLE public.comunidades (
    id integer NOT NULL,
    nombre character varying(50) NOT NULL,
    descripcion character varying(300)
);
    DROP TABLE public.comunidades;
       public         postgres    false    3            �            1259    16453    grupos    TABLE     �   CREATE TABLE public.grupos (
    id integer NOT NULL,
    nombre character varying(50) NOT NULL,
    descripcion character varying(300)
);
    DROP TABLE public.grupos;
       public         postgres    false    3            �            1259    16502    invitaciones    TABLE     �   CREATE TABLE public.invitaciones (
    id integer NOT NULL,
    usu_inv_id integer NOT NULL,
    juego_id integer NOT NULL,
    estado public.estadosoli NOT NULL
);
     DROP TABLE public.invitaciones;
       public         postgres    false    3    536            �            1259    16481    juegosComunidades    TABLE     �   CREATE TABLE public."juegosComunidades" (
    id integer NOT NULL,
    comunidad_id integer NOT NULL,
    juego_id integer NOT NULL,
    estado public."estadoPartida" NOT NULL
);
 '   DROP TABLE public."juegosComunidades";
       public         postgres    false    637    3            �            1259    16486    mensajes    TABLE     �   CREATE TABLE public.mensajes (
    id integer NOT NULL,
    chat_id integer,
    grupo_id integer,
    comunidad_id integer,
    remitente_id integer NOT NULL,
    contenido character varying(500) NOT NULL,
    fecha date NOT NULL
);
    DROP TABLE public.mensajes;
       public         postgres    false    3            �            1259    16438    notificaciones    TABLE     �   CREATE TABLE public.notificaciones (
    id integer NOT NULL,
    tipo public.tiponotif NOT NULL,
    emisorusu integer,
    emisoremp "char",
    receptor integer NOT NULL,
    estado public.estadosoli NOT NULL,
    fecha date NOT NULL
);
 "   DROP TABLE public.notificaciones;
       public         postgres    false    604    536    3            �            1259    16471    participanteCom    TABLE     j   CREATE TABLE public."participanteCom" (
    comunidad_id integer NOT NULL,
    usu_id integer NOT NULL
);
 %   DROP TABLE public."participanteCom";
       public         postgres    false    3            �            1259    16463    participantesGrupos    TABLE     �   CREATE TABLE public."participantesGrupos" (
    grupo_id integer NOT NULL,
    usu_id integer NOT NULL,
    rol public."rolGrupo" NOT NULL
);
 )   DROP TABLE public."participantesGrupos";
       public         postgres    false    625    3            �            1259    16494    partidas    TABLE     �   CREATE TABLE public.partidas (
    id integer NOT NULL,
    juego_id integer NOT NULL,
    usu_id integer NOT NULL,
    duracion interval NOT NULL,
    estado public."estadoPartida" NOT NULL,
    puntaje integer NOT NULL
);
    DROP TABLE public.partidas;
       public         postgres    false    3    637            �            1259    16507    trolls    TABLE     }   CREATE TABLE public.trolls (
    id integer NOT NULL,
    espectador_id integer NOT NULL,
    partida_id integer NOT NULL
);
    DROP TABLE public.trolls;
       public         postgres    false    3            �            1259    16394    usuario    TABLE     6  CREATE TABLE public.usuario (
    id integer NOT NULL,
    nombre character varying(50) NOT NULL,
    edad integer NOT NULL,
    correo character varying(50) NOT NULL,
    contrasenia character varying(100) NOT NULL,
    descripcion character varying(200),
    fecha_alta date NOT NULL,
    fecha_baja date
);
    DROP TABLE public.usuario;
       public         postgres    false    3            �            1259    16499 	   victorias    TABLE     ^   CREATE TABLE public.victorias (
    usu_id integer NOT NULL,
    juego_id integer NOT NULL
);
    DROP TABLE public.victorias;
       public         postgres    false    3            D          0    16435 	   actividad 
   TABLE DATA               :   COPY public.actividad (usuario_id, actividad) FROM stdin;
    public       postgres    false    197   �8       F          0    16443    amigos 
   TABLE DATA               D   COPY public.amigos (id, usu1_id, usu2_id, fecha_inicio) FROM stdin;
    public       postgres    false    199   �8       G          0    16448    chats 
   TABLE DATA               C   COPY public.chats (id, usu1_id, usu2_id, fecha_inicio) FROM stdin;
    public       postgres    false    200   �8       J          0    16466    comunidades 
   TABLE DATA               >   COPY public.comunidades (id, nombre, descripcion) FROM stdin;
    public       postgres    false    203   9       H          0    16453    grupos 
   TABLE DATA               9   COPY public.grupos (id, nombre, descripcion) FROM stdin;
    public       postgres    false    201   59       P          0    16502    invitaciones 
   TABLE DATA               H   COPY public.invitaciones (id, usu_inv_id, juego_id, estado) FROM stdin;
    public       postgres    false    209   R9       L          0    16481    juegosComunidades 
   TABLE DATA               Q   COPY public."juegosComunidades" (id, comunidad_id, juego_id, estado) FROM stdin;
    public       postgres    false    205   o9       M          0    16486    mensajes 
   TABLE DATA               g   COPY public.mensajes (id, chat_id, grupo_id, comunidad_id, remitente_id, contenido, fecha) FROM stdin;
    public       postgres    false    206   �9       E          0    16438    notificaciones 
   TABLE DATA               a   COPY public.notificaciones (id, tipo, emisorusu, emisoremp, receptor, estado, fecha) FROM stdin;
    public       postgres    false    198   �9       K          0    16471    participanteCom 
   TABLE DATA               A   COPY public."participanteCom" (comunidad_id, usu_id) FROM stdin;
    public       postgres    false    204   �9       I          0    16463    participantesGrupos 
   TABLE DATA               F   COPY public."participantesGrupos" (grupo_id, usu_id, rol) FROM stdin;
    public       postgres    false    202   �9       N          0    16494    partidas 
   TABLE DATA               S   COPY public.partidas (id, juego_id, usu_id, duracion, estado, puntaje) FROM stdin;
    public       postgres    false    207    :       Q          0    16507    trolls 
   TABLE DATA               ?   COPY public.trolls (id, espectador_id, partida_id) FROM stdin;
    public       postgres    false    210   :       C          0    16394    usuario 
   TABLE DATA               m   COPY public.usuario (id, nombre, edad, correo, contrasenia, descripcion, fecha_alta, fecha_baja) FROM stdin;
    public       postgres    false    196   ::       O          0    16499 	   victorias 
   TABLE DATA               5   COPY public.victorias (usu_id, juego_id) FROM stdin;
    public       postgres    false    208   W:       �
           2606    16447    amigos amigos_pkey 
   CONSTRAINT     U   ALTER TABLE ONLY public.amigos
    ADD CONSTRAINT amigos_pkey PRIMARY KEY (usu1_id);
 <   ALTER TABLE ONLY public.amigos DROP CONSTRAINT amigos_pkey;
       public         postgres    false    199            �
           2606    16452    chats chats_pkey 
   CONSTRAINT     N   ALTER TABLE ONLY public.chats
    ADD CONSTRAINT chats_pkey PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.chats DROP CONSTRAINT chats_pkey;
       public         postgres    false    200            �
           2606    16470    comunidades comunidades_pkey 
   CONSTRAINT     Z   ALTER TABLE ONLY public.comunidades
    ADD CONSTRAINT comunidades_pkey PRIMARY KEY (id);
 F   ALTER TABLE ONLY public.comunidades DROP CONSTRAINT comunidades_pkey;
       public         postgres    false    203            �
           2606    16457    grupos grupos_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.grupos
    ADD CONSTRAINT grupos_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.grupos DROP CONSTRAINT grupos_pkey;
       public         postgres    false    201            �
           2606    16506    invitaciones invitaciones_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public.invitaciones
    ADD CONSTRAINT invitaciones_pkey PRIMARY KEY (id);
 H   ALTER TABLE ONLY public.invitaciones DROP CONSTRAINT invitaciones_pkey;
       public         postgres    false    209            �
           2606    16485 (   juegosComunidades juegosComunidades_pkey 
   CONSTRAINT     j   ALTER TABLE ONLY public."juegosComunidades"
    ADD CONSTRAINT "juegosComunidades_pkey" PRIMARY KEY (id);
 V   ALTER TABLE ONLY public."juegosComunidades" DROP CONSTRAINT "juegosComunidades_pkey";
       public         postgres    false    205            �
           2606    16493    mensajes mensajes_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.mensajes
    ADD CONSTRAINT mensajes_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.mensajes DROP CONSTRAINT mensajes_pkey;
       public         postgres    false    206            �
           2606    16442 "   notificaciones notificaciones_pkey 
   CONSTRAINT     `   ALTER TABLE ONLY public.notificaciones
    ADD CONSTRAINT notificaciones_pkey PRIMARY KEY (id);
 L   ALTER TABLE ONLY public.notificaciones DROP CONSTRAINT notificaciones_pkey;
       public         postgres    false    198            �
           2606    16498    partidas partidas_pkey 
   CONSTRAINT     T   ALTER TABLE ONLY public.partidas
    ADD CONSTRAINT partidas_pkey PRIMARY KEY (id);
 @   ALTER TABLE ONLY public.partidas DROP CONSTRAINT partidas_pkey;
       public         postgres    false    207            �
           2606    16511    trolls trolls_pkey 
   CONSTRAINT     P   ALTER TABLE ONLY public.trolls
    ADD CONSTRAINT trolls_pkey PRIMARY KEY (id);
 <   ALTER TABLE ONLY public.trolls DROP CONSTRAINT trolls_pkey;
       public         postgres    false    210            �
           2606    16398    usuario usuario_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public.usuario
    ADD CONSTRAINT usuario_pkey PRIMARY KEY (id);
 >   ALTER TABLE ONLY public.usuario DROP CONSTRAINT usuario_pkey;
       public         postgres    false    196            D      x������ � �      F      x������ � �      G      x������ � �      J      x������ � �      H      x������ � �      P      x������ � �      L      x������ � �      M      x������ � �      E      x������ � �      K      x������ � �      I      x������ � �      N      x������ � �      Q      x������ � �      C      x������ � �      O      x������ � �     