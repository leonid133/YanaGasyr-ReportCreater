SELECT DATE_FORMAT( t1.DateTime, '%H:%i') as 'Время выхода в эфир', t1.filename as 'Наименование аудиоматериала (бренд )', 
alias.aliace as 'Категория а/мат ( рекл/ нерекл.)', 
'' as 'Вид  заказных, промо, анонсных аудиоматериалов, наименование заказчика,№ и дата договора', 
SEC_TO_TIME(t2.DateTime - t1.DateTime) as 'Хронометраж',
'' as'Примечания' FROM `cpp_data`.`dbm` as t1
join `cpp_data`.`dbm` as t2 on 
(
	 ( (t2.DateTime - t1.DateTime) <= DATE_ADD(t1.time2, INTERVAL 60 SECOND) )   
 and ( (t2.DateTime - t1.DateTime) > "00:00:00" ) 
 and "<" = t1.n 
 and ">" = t2.n 
 and t1.filename = t2.filename
 and t1.more0 = t2.more0
)
join `cpp_data`.`aliases` as alias on alias.aliace = t1.type
where t1.DateTime >= "2015-07-02"
and t1.DateTime < DATE_ADD("2015-07-02", INTERVAL 1 DAY)
order by t1.filename