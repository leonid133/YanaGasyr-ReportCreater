select 
_progname as 'Название программы', 
SEC_TO_TIME(sum(T.time_chr)) AS 'Хронометраж',
-- _begin,
 -- SEC_TO_TIME( sum( _end - _begin ) ) as 'Первый выход',
 -- _chrono AS 'Хронометраж',
 count( _progname ) AS 'Выходы',
T.*
 from (SELECT 
	-- tt.*,
	t1.DateTime AS _begin,
    t2.DateTime AS _end,
    SUBSTRING_INDEX(t1.locate, '\\', 1) AS _progname,
    -- SEC_TO_TIME( sum( t2.DateTime - t1.DateTime ) ) as 'Первый выход',
    SEC_TO_TIME(t2.DateTime - t1.DateTime) AS _chrono,
    sum(t2.time2) as time_chr,
    t1.`group`
   -- count( SUBSTRING_INDEX(t1.locate, '\\', 1) ) AS 'Выходы'
FROM
    `cpp_data`.`dbm` AS t1
        JOIN
    `cpp_data`.`dbm` AS t2 ON (((t2.DateTime - t1.DateTime) <= DATE_ADD(t1.time2, INTERVAL 60 SECOND))
        AND ((t2.DateTime - t1.DateTime) > '00:00:00')
        AND '<' = t1.n
        AND '>' = t2.n
        AND t1.filename = t2.filename
        AND t1.more0 = t2.more0)
/* left join `cpp_data`.`dbm` as tt on tt.DateTime = t2.DateTime 
and SUBSTRING_INDEX(tt.locate, '\\', 1) = SUBSTRING_INDEX(t1.locate, '\\', 1)	
AND '<' = tt.n	*/
WHERE
    t1.DateTime >= '2015-07-02'
        AND t1.DateTime < DATE_ADD('2015-07-02', INTERVAL 1 DAY)
        AND t1.type != 'ROTACIA' 
        AND t1.type != 'ATM'
 GROUP BY t1.`group`
 -- GROUP BY SUBSTRING_INDEX(t1.locate, '\\', 1)-- ) as T
) as T 
 -- order by T._begin
 GROUP BY T._progname
 