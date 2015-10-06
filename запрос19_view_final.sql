SET @granularity:=60*60;
SET @prepare_date:='2015-07-02 00:00:00';
SET GLOBAL innodb_lock_wait_timeout = 500000; 
SET innodb_lock_wait_timeout = 500000; 

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
SELECT `g`.`group` as `group_`, 
	time_to_sec(t2.time2) as _timetrack,
    `g`.DateTime AS _begin,
    t2.DateTime AS _end,
    SUBSTRING_INDEX(`g`.locate, '\\', 1) as _progname,
    time_to_sec(t2.DateTime - `g`.DateTime) AS _chrono, `g`.*
FROM `cpp_data`.`dbm` `g`
    JOIN
    `cpp_data`.`dbm` AS t2 ON (((t2.DateTime - `g`.DateTime) <= DATE_ADD(`g`.time2, INTERVAL 60 SECOND))
        AND ((t2.DateTime - `g`.DateTime) > '00:00:00')
        AND '<' = `g`.n
        AND '>' = t2.n
        AND `g`.filename = t2.filename
        AND `g`.more0 = t2.more0)
        AND t2.DateTime >= @prepare_date
        AND t2.DateTime < DATE_ADD(@prepare_date, INTERVAL 1 DAY)  AND t2.type != 'ROTACIA'  AND t2.type != 'ATM'
WHERE   `g`.DateTime >= @prepare_date
        AND `g`.DateTime < DATE_ADD(@prepare_date, INTERVAL 1 DAY)  AND `g`.type != 'ROTACIA'  AND `g`.type != 'ATM'
GROUP BY  `g`.DateTime, SUBSTRING_INDEX(`g`.locate, '\\', 1)
) as T 
group by T.`group_`
 ) as T_T
group by _uniqprogname
-- order by _begin
