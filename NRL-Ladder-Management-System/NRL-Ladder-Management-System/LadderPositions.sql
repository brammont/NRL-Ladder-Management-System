CREATE TABLE LadderPositions (
    position_id INT PRIMARY KEY,
    position INT,
    team_id INT FOREIGN KEY REFERENCES Teams(team_id),
    played INT,
    points INT,
    wins INT,
    drawn INT,
    lost INT,
    byes INT,
    for_score INT,
    against_score INT,
    diff INT
);

