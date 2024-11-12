CREATE TABLE Matches (
    match_id INT PRIMARY KEY,
    date DATE,
    team_1_id INT FOREIGN KEY REFERENCES Teams(team_id),
    team_2_id INT FOREIGN KEY REFERENCES Teams(team_id),
    score_team_1 INT,
    score_team_2 INT
);

