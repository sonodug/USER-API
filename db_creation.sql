CREATE DATABASE auth;

CREATE TABLE user_group (
   id SERIAL PRIMARY KEY,
   code VARCHAR(10) CHECK (code IN ('Admin', 'User')) NOT NULL,
   description VARCHAR(100) NOT NULL
);

CREATE TABLE user_state (
   id SERIAL PRIMARY KEY,
   code VARCHAR(10) CHECK (code IN ('Active', 'Blocked')) NOT NULL,
   description VARCHAR(100) NOT NULL
);

CREATE TABLE public.user (
   id SERIAL PRIMARY KEY,
   login VARCHAR(50) NOT NULL UNIQUE,
   password VARCHAR(100) NOT NULL,
   created_date TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
   user_group_id INTEGER NOT NULL REFERENCES user_group(id),
   user_state_id INTEGER NOT NULL REFERENCES user_state(id)
);