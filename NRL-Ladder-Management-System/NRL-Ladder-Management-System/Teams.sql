CREATE TABLE Teams (
    team_id INT PRIMARY KEY IDENTITY(1,1),  -- Auto-incrementing identity column
    team_name NVARCHAR(50) NOT NULL,
    home_ground NVARCHAR(50)
);


