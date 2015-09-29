 SET @granularity:=60*60;
select _uniqprogname as 'Название программы',
SEC_TO_TIME( _sumtime ) as 'Первый выход',
SEC_TO_TIME( sum( _sumtime ) ) as 'Хронометраж факт',
-- SEC_TO_TIME( sum( _timetrack ) ),
-- _timetrack,
-- _sum_timetrack,
SEC_TO_TIME( sum( _sum_timetrack ) ) as 'Хронометраж теоретический',
count( _uniqprogname ) AS 'Выходы' -- , 
-- T_T.*
from(
select 
sum( _timetrack ) as _sum_timetrack,
_progname as _uniqprogname, 
 _begin as _b, _end as _e,
  sum( _chrono ) as _sumtime,
 _chrono AS _chronoLocal,
  count( _progname ) AS 'Выходы',
T.*
 from (

SELECT MIN(`g`.DateTime) AS `group_`, 
	time_to_sec(`f`.time2) as _timetrack,
    `f`.DateTime AS _begin,
    t2.DateTime AS _end,
    SUBSTRING_INDEX(`f`.locate, '\\', 1) as _progname,
    time_to_sec(t2.DateTime - `f`.DateTime) AS _chrono, `f`.*
FROM `cpp_data`.`dbm` `g`
    CROSS JOIN `cpp_data`.`dbm` `f`
    ON (SUBSTRING_INDEX(`f`.locate, '\\', 1) = SUBSTRING_INDEX(`g`.locate, '\\', 1)
        AND `f`.DateTime BETWEEN `g`.DateTime AND `g`.DateTime+@granularity and `f`.DateTime >= '2015-07-02'
        AND `f`.DateTime < DATE_ADD('2015-07-02', INTERVAL 1 DAY)  AND `f`.type != 'ROTACIA'  AND `f`.type != 'ATM'
    )
    JOIN
    `cpp_data`.`dbm` AS t2 ON (((t2.DateTime - `f`.DateTime) <= DATE_ADD(`f`.time2, INTERVAL 60 SECOND))
        AND ((t2.DateTime - `f`.DateTime) > '00:00:00')
        AND '<' = `f`.n
        AND '>' = t2.n
        AND `f`.filename = t2.filename
        AND `f`.more0 = t2.more0)
        AND t2.DateTime >= '2015-07-02'
        AND t2.DateTime < DATE_ADD('2015-07-02', INTERVAL 1 DAY)  AND t2.type != 'ROTACIA'  AND t2.type != 'ATM'
-- WHERE -- `f`.`group` IS NULL OR
WHERE   `f`.DateTime >= '2015-07-02'
        AND `f`.DateTime < DATE_ADD('2015-07-02', INTERVAL 1 DAY)  AND `f`.type != 'ROTACIA'  AND `f`.type != 'ATM'
        and `g`.DateTime >= '2015-07-02'
        AND `g`.DateTime < DATE_ADD('2015-07-02', INTERVAL 1 DAY)  AND `g`.type != 'ROTACIA'  AND `g`.type != 'ATM'
-- `f`.`tm` >= (UNIX_TIMESTAMP()-2*@granularity)
GROUP BY `f`.DateTime, SUBSTRING_INDEX(`f`.locate, '\\', 1)
) as T 
group by `group_`
 -- order by _progname, _begin
 ) as T_T
 group by _uniqprogname
 -- order by _progname, T_T._begin 