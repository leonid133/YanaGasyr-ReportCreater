SELECT 
    DATE_FORMAT(t1.DateTime, '%H:%i') AS 'Время выхода в эфир',
    TRIM(TRAILING '.' FROM TRIM(TRAILING SUBSTRING_INDEX(t1.filename, '.', -1) FROM t1.filename) ) AS 'Наименование аудиоматериала (бренд )',
    alias.aliace AS 'Категория а/мат ( рекл/ нерекл.)',
    '' AS 'Вид  заказных, промо, анонсных аудиоматериалов, наименование заказчика,№ и дата договора',
    SEC_TO_TIME(t2.DateTime - t1.DateTime) AS 'Хронометраж',
    '' AS 'Примечания'
FROM
    `cpp_data`.`dbm` AS t1
        JOIN
    `cpp_data`.`dbm` AS t2 ON (((t2.DateTime - t1.DateTime) <= DATE_ADD(t1.time2, INTERVAL 60 SECOND))
        AND ((t2.DateTime - t1.DateTime) > '00:00:00')
        AND '<' = t1.n
        AND '>' = t2.n
        AND t1.filename = t2.filename
        AND t1.more0 = t2.more0)
        JOIN
    `cpp_data`.`aliases` AS alias ON alias.aliace = t1.type
WHERE
    t1.DateTime >= '2015-07-02'
        AND t1.DateTime < DATE_ADD('2015-07-02', INTERVAL 1 DAY)
        AND t1.type != 'ROTACIA' 
        AND t1.type != 'ATM'
ORDER BY t1.filename, t1.DateTime