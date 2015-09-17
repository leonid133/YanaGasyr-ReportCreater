create view (SELECT 
    SUBSTRING_INDEX(t1.locate, '\\', 1) AS 'Наименование программ',
    SEC_TO_TIME( sum( t2.DateTime - t1.DateTime ) ) as 'Первый выход',
    SEC_TO_TIME(t2.DateTime - t1.DateTime) AS 'Хронометраж',
    count(t1.locate) AS 'Выходы'
FROM
    `cpp_data`.`dbm` AS t1
        JOIN
    `cpp_data`.`dbm` AS t2 ON (((t2.DateTime - t1.DateTime) <= DATE_ADD(t1.time2, INTERVAL 60 SECOND))
        AND ((t2.DateTime - t1.DateTime) > '00:00:00')
        AND '<' = t1.n
        AND '>' = t2.n
        AND t1.filename = t2.filename
        AND t1.more0 = t2.more0)
		
WHERE
    t1.DateTime >= '2015-07-02'
        AND t1.DateTime < DATE_ADD('2015-07-02', INTERVAL 1 DAY)
        AND t1.type != 'ROTACIA' 
        AND t1.type != 'ATM'
GROUP BY SUBSTRING_INDEX(t1.locate, '\\', 1) ) as T
-- ORDER BY t1.filename, t1.DateTime
     select count(*) from T as t3 where
     -- (`cpp_data`.`dbm`.DateTime - t2.DateTime) >='00:04:00' AND 
     -- (`cpp_data`.`dbm`.DateTime - t2.DateTime) <='00:30:00' and
     SUBSTRING_INDEX( t3.locate, '\\', 1) = SUBSTRING_INDEX(T.locate, '\\', 1);
    
