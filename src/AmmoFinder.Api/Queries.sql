﻿select * from products 
where caliber is null
--and brand is null
--order by inventory desc
--and RetailerProductId like '%3074457345618960344%'
--where roundtype is null
--where Caliber is null
--where name like '%cci%'
--where name like '%sierra%'
--where name like '%30cal%'
--where RoundCount is null
--where description like '%Bushmaster%'
--where name like '%7.62%'

--select * from retailers

--delete from products where id > 0
--delete from retailers where id > 0

SELECT retailerproductid, retailerid, COUNT(*)
FROM products
GROUP BY retailerproductid,retailerid
HAVING COUNT(*) > 1


select * from retailers

select * from products
where retailerproductid = 'bulk-32-acp-ammo-32auto65hsjhpfed-20'


