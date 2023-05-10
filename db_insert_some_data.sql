INSERT INTO user_group (code, description) VALUES
('Admin', 'Администратор'),
('User', 'Пользователь');

INSERT INTO user_state (code, description) VALUES
('Active', 'Активный'),
('Blocked', 'Заблокированный');

INSERT INTO public.user (login, password, user_group_id, user_state_id) VALUES
('admin', 'admin123', 3, 3),
('user1', 'pass123', 4, 3),
('user2', 'qwerty', 4, 3);