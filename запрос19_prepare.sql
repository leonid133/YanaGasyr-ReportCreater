SET @granularity:=60*60;
SET @prepare_date:=2015-07-02;
SET GLOBAL innodb_lock_wait_timeout = 500000; 
SET innodb_lock_wait_timeout = 500000; 

 UPDATE `cpp_data`.`dbm` `_f` CROSS JOIN (
SELECT MIN(`g`.DateTime) AS `group_`, 
	`f`.*
FROM `cpp_data`.`dbm` `g`
    CROSS JOIN `cpp_data`.`dbm` `f`
    ON (SUBSTRING_INDEX(`f`.locate, '\\', 1) = SUBSTRING_INDEX(`g`.locate, '\\', 1)
        AND `f`.DateTime BETWEEN `g`.DateTime AND `g`.DateTime+@granularity 
    )
 
  WHERE   -- `f`.`group` IS NULL 
 `f`.DateTime >= @prepare_date
        AND `f`.DateTime < DATE_ADD(@prepare_date, INTERVAL 1 DAY)
        AND `f`.type != 'ROTACIA' 
        AND `f`.type != 'ATM'  
       --  GROUP BY `f`.DateTime, `f`.more0 -- SUBSTRING_INDEX(`f`.locate, '\\', 1)
        GROUP BY `f`.DateTime  , SUBSTRING_INDEX(`f`.`locate`, '\\', 1)
  ) g_ ON ( time_to_sec(g_.`DateTime`) = time_to_sec(`_f`.`DateTime`) and g_.`n` = `_f`.`n` and g_.`more0` = `_f`.`more0` )
 and
`_f`.DateTime >= @prepare_date
        AND `_f`.DateTime < DATE_ADD(@prepare_date, INTERVAL 1 DAY)
        AND `_f`.type != 'ROTACIA' 
        AND `_f`.type != 'ATM' 
  SET `_f`.`group`=`g_`.`group_`;