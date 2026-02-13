-- Create wealthmanager if it doesn't exist (for clients using no-underscore name)
SELECT 'CREATE DATABASE wealth_manager'
WHERE NOT EXISTS (SELECT FROM pg_database WHERE datname = 'wealth_manager')\gexec
