CREATE FUNCTION check_admin_limit() RETURNS TRIGGER AS $$
BEGIN
  IF NEW.user_group_id = (SELECT id FROM user_group WHERE code = 'Admin') AND
     EXISTS(SELECT 1 FROM public.user WHERE user_group_id = (SELECT id FROM user_group WHERE code = 'Admin'))
  THEN
    RAISE EXCEPTION 'Cannot have more than one user with Admin group';
  END IF;
  RETURN NEW;
END;
$$ LANGUAGE plpgsql;

CREATE TRIGGER admin_limit_trigger
BEFORE INSERT OR UPDATE ON public.user
FOR EACH ROW
EXECUTE FUNCTION check_admin_limit();