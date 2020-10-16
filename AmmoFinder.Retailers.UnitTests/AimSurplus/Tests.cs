﻿using AmmoFinder.Retailers.AimSurplus;
using AutoMapper;
using Xunit;

namespace AmmoFinder.Retailers.UnitTests.AimSurplus
{
    public class Tests
    {
        private IMapper CreateMapper()
        {
            var mapperConfiguration = new MapperConfiguration(config =>
            {
                config.AddProfile<MapProfile>();
            });

            return new Mapper(mapperConfiguration);
        }

        [Fact]
        public void AutoMapperProfile_IsValid()
        {
            // Arrange
            var mapper = CreateMapper();

            // Act

            // Assert
            mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        //[Fact]
        //public void Fetch_IsValid()
        //{
        //    // Arrange
        //    var mapper = CreateMapper();
        //    var mockedHttp = new MockHttpMessageHandler();
        //    mockedHttp.When(Extension.BaseUrl)
        //        .Respond("application/json", "{\"page\":\"1\",\"pages\":10,\"featured\":null,\"suggestions\":[],\"products\":[{\"id\":5565,\"name\":\"Prvi Partizan .308/7.62x51 M80 145grn FMJ 20rd Box\",\"description\":\"<p>New PPU .308/7.62x51cal M80 ammunition by Prvi Partizan. Features a 145grn lead core full copper jacketed bullet, brass case, and non-corrosive boxer primer. Packaged 20rds to a box, and 25 Boxes (500rds) per Case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/5565/images/26561/308__85187.1589904616.220.290.png?c=2\",\"price\":\"15.95\",\"sale_price\":\"0.00\",\"total_sold\":20355,\"review_score\":\"4.88\",\"review_count\":24,\"url\":\"https://aimsurplus.com/prvi-partizan-308-7-62x51-m80-145grn-fmj-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:20:47.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":2138,\"score\":0},{\"id\":6135,\"name\":\"Prvi Partizan PPU 7.62x54R 182grn FMJ BT 20rd Box\",\"description\":\"<p>New 7.62x54R ammunition by Prvi Partizan (PPU) of Serbia. Features a 182grn lead core full copper jacketed boat tail bullet, brass case, and non-corrosive boxer primer. Packaged 20rds to a box and 200rds (10 boxes) to a case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/6135/images/12566/ap76254fmjbt182__69472.1540333844.220.290.jpg?c=2\",\"price\":\"14.95\",\"sale_price\":\"0.00\",\"total_sold\":5004,\"review_score\":\"5.00\",\"review_count\":5,\"url\":\"https://aimsurplus.com/prvi-partizan-ppu-7-62x54r-182grn-fmj-bt-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:23:51.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":701,\"score\":0},{\"id\":5725,\"name\":\"Prvi Partizan PPU \"Defense\" .357 Magnum 158gr JHP 50rd Box\",\"description\":\"<p>Part of the new PPU \"Defense\" line of Hollow Point high performance ammunition this JHP .357 Magnum ammunition by Prvi Partizan (PPU) features a 158grn lead core copper jacketed hollow point bullet, brass case, and non-corrosive boxer primer. Packaged 50rds to a box, and 500rds (10 boxes) to a case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/5725/images/25852/AP357MJHP158__18305.1582667238.220.290.png?c=2\",\"price\":\"29.95\",\"sale_price\":\"0.00\",\"total_sold\":4460,\"review_score\":\"5.00\",\"review_count\":8,\"url\":\"https://aimsurplus.com/prvi-partizan-ppu-defense-357-magnum-158gr-jhp-50rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:21:37.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":6,\"score\":0},{\"id\":5700,\"name\":\"Prvi Partizan PPU 7.62x54R 150grn SP 20rd Box\",\"description\":\"<p>New 7.62x54R ammunition by Prvi Partizan (PPU). Features a 150grn lead core copper jacketed soft point bullet, brass case, and non-corrosive boxer primer. Packaged 20rds to a box and 200rds (10 boxes) to a case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/5700/images/11781/appu76254sp150__85027.1540332857.220.290.jpg?c=2\",\"price\":\"14.95\",\"sale_price\":\"0.00\",\"total_sold\":3788,\"review_score\":\"5.00\",\"review_count\":6,\"url\":\"https://aimsurplus.com/prvi-partizan-ppu-7-62x54r-150grn-sp-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:21:30.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":274,\"score\":0},{\"id\":6132,\"name\":\"Prvi 8mm Kurz 124grn 20rd Box\",\"description\":\"<p>New production 8mm Kurz (7.92x33) ammunition by Prvi Partizan of Serbia. Features a 124grn Full copper jacketed lead core boat tail bullet, Brass case, Boxer primer, and is Non-corrosive. Packaged 20rds/box, 200rds (10 boxes)/case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/6132/images/12563/AP8KFMJ__97372.1540333840.220.290.jpg?c=2\",\"price\":\"15.95\",\"sale_price\":\"0.00\",\"total_sold\":1427,\"review_score\":\"5.00\",\"review_count\":2,\"url\":\"https://aimsurplus.com/prvi-8mm-kurz-124grn-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:23:49.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":106,\"score\":0},{\"id\":4624,\"name\":\"WOLF .30-06 FMJ 145grn Ammunition 20rd Box\",\"description\":\"<p>New Wolf Performance Ammunition Russian Production Commercial .30-06 Springfield ammunition. Features a 145grn Lead Core Full Bi-Metal Jacketed Bullet, Steel Case, and Non-Corrosive Berdan Primer. Packaged 20rds to a box, and 500rds (25 boxes) to a case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/4624/images/27398/AW3006FMJ145__01205.1597434935.220.290.png?c=2\",\"price\":\"14.95\",\"sale_price\":\"0.00\",\"total_sold\":824,\"review_score\":null,\"review_count\":0,\"url\":\"https://aimsurplus.com/wolf-30-06-fmj-145grn-ammunition-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:14:42.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":349,\"score\":0},{\"id\":9231,\"name\":\"Prvi Partizan PPU .50cal BMG M33 625grn FMJ Ammunition 5rd Box\",\"description\":\"<p>PPU .50 BMG, M33&nbsp;625grn FMJ ammunition. Brass case, Boxer primer, 5rd box.&nbsp;Militaries around the world have relied upon PPU Mil-Spec ammunition for decades. Recognized for dependability, PPU Mil-Spec ammunition is manufactured using the highest grade materials and quality production control, ensuring precision performance in accordance with military specifications.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/9231/images/27659/AP50FMJ625__47044.1599157108.220.290.png?c=2\",\"price\":\"16.95\",\"sale_price\":\"0.00\",\"total_sold\":485,\"review_score\":null,\"review_count\":0,\"url\":\"https://aimsurplus.com/prvi-partizan-ppu-50cal-bmg-m33-625grn-fmj-ammunition-5rd-box/\",\"created_at\":{\"date\":\"2020-08-27 16:56:48.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":475,\"score\":0},{\"id\":9027,\"name\":\"Federal .50cal BMG M33 Ball / M17 Tracer Ammunition 100rd Can\",\"description\":\"<p>Federal .50 BMG M33 Ball / M17 Tracer (4:1 Link), 100rd M2A1 Can,&nbsp;M33 = 661 grain (42.8 g) Full Metal Jacket.&nbsp;M17 = 618 grain (40.0 g) Full Metal Jacket. 100rd Can.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/9027/images/26949/AFEDZSAMA557MOI__77075.1594845153.220.290.png?c=2\",\"price\":\"369.95\",\"sale_price\":\"0.00\",\"total_sold\":85,\"review_score\":null,\"review_count\":0,\"url\":\"https://aimsurplus.com/federal-50cal-bmg-m33-ball-m17-tracer-ammunition-100rd-can/\",\"created_at\":{\"date\":\"2020-07-09 16:02:21.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":11,\"score\":0},{\"id\":9391,\"name\":\"Federal American Eagle .45 GAP 230grn FMJ Ammunition 50rd box\",\"description\":\"<p>New Production&nbsp;Federal American Eagle .45 GAP 230grn FMJ Ammunition.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/9391/images/28046/AFED45GAP__89345.1602273333.220.290.png?c=2\",\"price\":\"29.95\",\"sale_price\":\"0.00\",\"total_sold\":37,\"review_score\":null,\"review_count\":0,\"url\":\"https://aimsurplus.com/federal-american-eagle-45-gap-230grn-fmj-ammunition-50rd-box/\",\"created_at\":{\"date\":\"2020-10-09 12:10:40.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":363,\"score\":0},{\"id\":9389,\"name\":\"Prvi Partizan PPU 5.56X45 M855 62grn Green Tip Ammunition 1,000rd can\",\"description\":\"<p>New PPU 5.56x45 (.223) M855 ammunition by Prvi Partizan. Features a green tip 62grn copper jacketed bullet with steel penetrator, brass case, and non-corrosive boxer primer. Packaged 20rds to a box and 1,000rds (50 boxes) to a new, resealable M2A1 .50cal ammo can. Some of the best M855 available!</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/9389/images/28075/APM855CAN__95320.1602791560.220.290.png?c=2\",\"price\":\"649.95\",\"sale_price\":\"0.00\",\"total_sold\":16,\"review_score\":null,\"review_count\":0,\"url\":\"https://aimsurplus.com/prvi-partizan-ppu-5-56x45-m855-62grn-green-tip-ammunition-1-000rd-can/\",\"created_at\":{\"date\":\"2020-10-08 15:05:08.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":40,\"score\":0},{\"id\":6332,\"name\":\"WOLF .223 55grn FMJ 20rd Box\",\"description\":\"<p>*** Firm Limit of 50 boxes per customer***.&nbsp;&nbsp; New Russian production WPA (WOLF Performance Ammunition) .223 ammunition. Features a 55grn full bimetal jacketed bullet, polymer coated steel case, and non-corrosive berdan primer. Packaged 20rds to a box.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/6332/images/12843/aw22fmj55__03646.1542377755.220.290.jpg?c=2\",\"price\":\"7.99\",\"sale_price\":\"0.00\",\"total_sold\":346554,\"review_score\":\"4.91\",\"review_count\":175,\"url\":\"https://aimsurplus.com/wolf-223-55grn-fmj-20rd-box/\",\"created_at\":{\"date\":\"2018-12-20 10:27:06.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":4344,\"name\":\"WOLF GOLD .223/5.56X45 55grn FMJ 20rd box\",\"description\":\"<p>WOLF GOLD Performance Ammunition brand .223/5.56X45 ammunition. Features a full copper jacketed lead core 55grn bullet, brass case and reloadable non-corrosive boxer primer. Packaged 20rds to a box and 1,000rds (50 boxes) to a case. This is fantastic ammo at a super price! Wolf Factory Sale in Progress on Wolf Gold!</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/4344/images/15678/WOLFGOLD__54790.1549639454.220.290.jpg?c=2\",\"price\":\"7.99\",\"sale_price\":\"0.00\",\"total_sold\":278652,\"review_score\":\"4.91\",\"review_count\":76,\"url\":\"https://aimsurplus.com/wolf-gold-223-5-56x45-55grn-fmj-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:12:53.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":6118,\"name\":\"WOLF 7.62x39 FMJ 20rd Box\",\"description\":\"<p>**Firm Limit of 50 boxes per customer**&nbsp;&nbsp; New WOLF 7.62x39 ammunition. Features a Lead Core Full Bi-Metal Jacketed Bullet, Steel Case, and Non-Corrosive Berdan Primer. Packaged 20rds to a box, and 1,000rds (50 boxes) to a case.&nbsp;&nbsp; Packaging will be black box or camo box, the ammo is EXACTLY the same.&nbsp;&nbsp;</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/6118/images/15479/AW76239FMJ123sq1__25581.1547839804.220.290.jpg?c=2\",\"price\":\"6.49\",\"sale_price\":\"0.00\",\"total_sold\":251316,\"review_score\":\"4.91\",\"review_count\":76,\"url\":\"https://aimsurplus.com/wolf-7-62x39-fmj-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:23:45.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":6113,\"name\":\"WOLF 9mm 115grn FMJ 50rd Box\",\"description\":\"<p>** Firm limit of 10 boxes per customer**&nbsp; New Wolf Brand 9mm ammunition manufactured in Russia. Features a 115grn lead core full bi-metal jacketed bullet, polymer coated steel case, and non-corrosive berdan primer.</p>\r\n<p>&nbsp;</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/6113/images/15143/AW9FMJ115sq__25700.1546264899.220.290.png?c=2\",\"price\":\"16.95\",\"sale_price\":\"0.00\",\"total_sold\":138040,\"review_score\":\"4.86\",\"review_count\":253,\"url\":\"https://aimsurplus.com/wolf-9mm-115grn-fmj-50rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:23:44.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":6117,\"name\":\"WOLF 7.62x39 123Grn HP 20rd Box\",\"description\":\"<p>WOLF 7.62x39 ammunition. Features a Lead Core Bi-Metal Jacketed Hollow Point Bullet, Steel Case, and Non-Corrosive Berdan Primer. Packaged 20rds to a box, and 1,000rds (50 boxes) to a case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/6117/images/12537/aw76239hp__90363.1540333811.220.290.jpg?c=2\",\"price\":\"6.49\",\"sale_price\":\"0.00\",\"total_sold\":106139,\"review_score\":\"4.89\",\"review_count\":36,\"url\":\"https://aimsurplus.com/wolf-7-62x39-123grn-hp-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:23:45.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":4034,\"name\":\"Aguila .22LR High Velocity 40grn Copper Coated 50rd Box\",\"description\":\"<p>Aguila .22LR High Velocity Solid Point 40gr Copper Coated 50rd Box. These high velocity rounds are perfect for target shooting or plinking and provide tight groupings. The copper-plated bullet provides excellent accuracy, consistent performance, and smooth cycling. Packaged 50rds/Box, 500rds/Brick and 5000rds/Case</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/4034/images/16641/AG22HVSP40SQ1__42250.1553711280.220.290.jpg?c=2\",\"price\":\"2.59\",\"sale_price\":\"0.00\",\"total_sold\":93352,\"review_score\":\"4.94\",\"review_count\":33,\"url\":\"https://aimsurplus.com/aguila-22lr-high-velocity-40grn-copper-coated-50rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:11:14.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":3151,\"name\":\"WPA (WOLF Performance Arms) 6.5 Grendel 100grn FMJ 20rd Box\",\"description\":\"<p>WPA (WOLF Performance Ammunition) 6.5 Grendel Ammunition. Features a 100grn full bi-metal jacketed bullet, lacquer coated steel case and non-corrosive Berdan primer. Packaged 20rds to a box and 500rds (25 boxes) to a case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/3151/images/16672/AW65FMJ100X__49863.1553713695.220.290.jpg?c=2\",\"price\":\"7.95\",\"sale_price\":\"0.00\",\"total_sold\":49010,\"review_score\":\"4.78\",\"review_count\":36,\"url\":\"https://aimsurplus.com/wpa-wolf-performance-arms-6-5-grendel-100grn-fmj-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:04:59.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":3664,\"name\":\"Prvi Partizan PPU 9mm 124grn FMJ 50rd Box\",\"description\":\"<p>New PPU brand 9mm ammunition by Prvi Partizan of Serbia. Features a 124grn lead core full copper jacketed bullet, brass case, and non-corrosive boxer primer. Packaged 50rds to box, and 1,000rds (20 boxes) to a case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/3664/images/7278/ap9fmj124f__45158.1540323015.220.290.jpg?c=2\",\"price\":\"19.95\",\"sale_price\":\"0.00\",\"total_sold\":42178,\"review_score\":null,\"review_count\":0,\"url\":\"https://aimsurplus.com/prvi-partizan-ppu-9mm-124grn-fmj-50rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:09:01.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":5588,\"name\":\"Prvi Partizan PPU 5.56X45 M855 62grn Green Tip Ammunition 20rd box\",\"description\":\"<p>New PPU 5.56x45 (.223) M855 ammunition by Prvi Partizan. Features a green tip 62grn copper jacketed bullet with steel penetrator, brass case, and non-corrosive boxer primer. Packaged 20rds to a box and 1,000rds (50 boxes) to a case. Some of the best M855 available!</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/5588/images/28074/AP556M855__70841.1602790526.220.290.png?c=2\",\"price\":\"11.99\",\"sale_price\":\"0.00\",\"total_sold\":40750,\"review_score\":\"4.80\",\"review_count\":5,\"url\":\"https://aimsurplus.com/prvi-partizan-ppu-5-56x45-m855-62grn-green-tip-ammunition-20rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:20:55.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":3858,\"name\":\"Aguila .22LR Standard Velocity Solid Point 40grn Lead 50rd Box\",\"description\":\"<p>Aguila Standard Velocity Solid Point 40gr Lead 50rd Box. Aguila's standard velocity round is guaranteed to deliver optimal performance at an all-too-affordable price, making it the perfect plinking or target round. Packaged 50rds/Box, 500rds/Brick and 5000rds/Case</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/3858/images/25029/AG22SV40__60557.1576530162.220.290.png?c=2\",\"price\":\"2.49\",\"sale_price\":\"0.00\",\"total_sold\":35980,\"review_score\":\"4.85\",\"review_count\":13,\"url\":\"https://aimsurplus.com/aguila-22lr-standard-velocity-solid-point-40grn-lead-50rd-box/\",\"created_at\":{\"date\":\"2018-11-19 19:10:15.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0},{\"id\":8576,\"name\":\"STV Technology Scorpio 9mm 124grn FMJ Ammunition 50rd Box\",\"description\":\"<p>New production STV Technology Scorpio 9mm 124grn FMJ Ammunition. Manufactured in the Czech Republic. This ammunition features a 124grn lead core, copper jacketed bullet and brass case with non-corrosive boxer primer. Packaged 50rds to a box, 1,000rds (20 boxes) to a case.</p>\",\"thumbnail\":\"https://cdn11.bigcommerce.com/s-admq3scrtq/products/8576/images/25775/AS9A__98924.1582127485.220.290.png?c=2\",\"price\":\"19.95\",\"sale_price\":\"0.00\",\"total_sold\":29915,\"review_score\":\"5.00\",\"review_count\":8,\"url\":\"https://aimsurplus.com/stv-technology-scorpio-9mm-124grn-fmj-ammunition-50rd-box/\",\"created_at\":{\"date\":\"2020-02-14 12:05:25.000000\",\"timezone_type\":3,\"timezone\":\"America/New_York\"},\"inventory\":0,\"score\":0}],\"facets\":[{\"id\":625,\"name\":\"Manufacturer\",\"display_name\":\"Brand\",\"handle\":null,\"is_enabled\":1,\"category_id\":30,\"position\":2,\"sort_by\":\"count\",\"created_at\":\"2019-12-30 03:01:27\",\"updated_at\":\"2020-01-17 16:49:04\",\"products\":183,\"options\":[{\"name\":\"Prvi Partizan\",\"products\":91,\"chosen\":false},{\"name\":\"Wolf\",\"products\":15,\"chosen\":false},{\"name\":\"Federal\",\"products\":14,\"chosen\":false},{\"name\":\"Norma\",\"products\":11,\"chosen\":false},{\"name\":\"Aguila\",\"products\":7,\"chosen\":false},{\"name\":\"CCI\",\"products\":7,\"chosen\":false},{\"name\":\"Red Army Standard\",\"products\":6,\"chosen\":false},{\"name\":\"Sellier & Bellot\",\"products\":6,\"chosen\":false},{\"name\":\"Winchester\",\"products\":4,\"chosen\":false},{\"name\":\"CCI Blazer\",\"products\":3,\"chosen\":false},{\"name\":\"Magtech\",\"products\":2,\"chosen\":false},{\"name\":\"PMC\",\"products\":2,\"chosen\":false},{\"name\":\"Prvi Patizan\",\"products\":2,\"chosen\":false},{\"name\":\"Blazer\",\"products\":1,\"chosen\":false},{\"name\":\"CCI Speer\",\"products\":1,\"chosen\":false},{\"name\":\"Estate Catridge\",\"products\":1,\"chosen\":false},{\"name\":\"Federal American Eagle\",\"products\":1,\"chosen\":false},{\"name\":\"Federal Champion\",\"products\":1,\"chosen\":false},{\"name\":\"Geco\",\"products\":1,\"chosen\":false},{\"name\":\"Hotshot\",\"products\":1,\"chosen\":false},{\"name\":\"MEDEF Venom\",\"products\":1,\"chosen\":false},{\"name\":\"Remington\",\"products\":1,\"chosen\":false},{\"name\":\"SIG\",\"products\":1,\"chosen\":false},{\"name\":\"STV Technology\",\"products\":1,\"chosen\":false},{\"name\":\"Speer Gold Dot\",\"products\":1,\"chosen\":false},{\"name\":\"TulAmmo\",\"products\":1,\"chosen\":false}]},{\"id\":647,\"name\":\"Application\",\"display_name\":\"Application\",\"handle\":null,\"is_enabled\":1,\"category_id\":30,\"position\":3,\"sort_by\":\"count\",\"created_at\":\"2019-12-30 03:01:27\",\"updated_at\":\"2020-01-17 16:49:04\",\"products\":127,\"options\":[{\"name\":\"Target\",\"products\":71,\"chosen\":false},{\"name\":\"Hunting\",\"products\":31,\"chosen\":false},{\"name\":\"Self Defense\",\"products\":21,\"chosen\":false},{\"name\":\"Match\",\"products\":3,\"chosen\":false},{\"name\":\"Subsonic\",\"products\":1,\"chosen\":false}]},{\"id\":1134,\"name\":\"In Stock\",\"display_name\":\"In Stock\",\"handle\":\"in-stock\",\"is_enabled\":1,\"category_id\":30,\"position\":4,\"sort_by\":\"count\",\"created_at\":null,\"updated_at\":\"2020-01-17 16:49:04\",\"products\":10,\"options\":[{\"name\":\"In Stock\",\"products\":10,\"chosen\":false}]},{\"id\":648,\"name\":\"Box Quantity\",\"display_name\":\"Box Quantity\",\"handle\":null,\"is_enabled\":1,\"category_id\":30,\"position\":8,\"sort_by\":\"count\",\"created_at\":\"2019-12-30 03:01:27\",\"updated_at\":\"2020-01-17 16:49:04\",\"products\":160,\"options\":[{\"name\":\"20rds\",\"products\":79,\"chosen\":false},{\"name\":\"50rds\",\"products\":72,\"chosen\":false},{\"name\":\"30rds\",\"products\":2,\"chosen\":false},{\"name\":\"5rds\",\"products\":2,\"chosen\":false},{\"name\":\"20rd\",\"products\":1,\"chosen\":false},{\"name\":\"25rds\",\"products\":1,\"chosen\":false},{\"name\":\"500rds\",\"products\":1,\"chosen\":false},{\"name\":\"525rds\",\"products\":1,\"chosen\":false},{\"name\":\"5rd\",\"products\":1,\"chosen\":false}]},{\"id\":649,\"name\":\"Bullet Type\",\"display_name\":\"Bullet Type\",\"handle\":null,\"is_enabled\":1,\"category_id\":30,\"position\":9,\"sort_by\":\"count\",\"created_at\":\"2019-12-30 03:01:27\",\"updated_at\":\"2020-01-17 16:49:04\",\"products\":162,\"options\":[{\"name\":\"FMJ\",\"products\":92,\"chosen\":false},{\"name\":\"SP\",\"products\":20,\"chosen\":false},{\"name\":\"JHP\",\"products\":19,\"chosen\":false},{\"name\":\"HP\",\"products\":8,\"chosen\":false},{\"name\":\"LRN\",\"products\":7,\"chosen\":false},{\"name\":\"00 Buck\",\"products\":2,\"chosen\":false},{\"name\":\"MHP\",\"products\":2,\"chosen\":false},{\"name\":\"BTHP\",\"products\":1,\"chosen\":false},{\"name\":\"Buckshot\",\"products\":1,\"chosen\":false},{\"name\":\"Frangible\",\"products\":1,\"chosen\":false},{\"name\":\"HST\",\"products\":1,\"chosen\":false},{\"name\":\"JSP\",\"products\":1,\"chosen\":false},{\"name\":\"Jacketed Hollow Point\",\"products\":1,\"chosen\":false},{\"name\":\"Lead\",\"products\":1,\"chosen\":false},{\"name\":\"Semi-Wadccutter HP\",\"products\":1,\"chosen\":false},{\"name\":\"Semi-Wadcutter Hollow Point\",\"products\":1,\"chosen\":false},{\"name\":\"Slug\",\"products\":1,\"chosen\":false},{\"name\":\"TSJ\",\"products\":1,\"chosen\":false},{\"name\":\"VMAX Polymer Tip\",\"products\":1,\"chosen\":false}]},{\"id\":657,\"name\":\"Case Quantity\",\"display_name\":\"Case Quantity\",\"handle\":null,\"is_enabled\":1,\"category_id\":30,\"position\":10,\"sort_by\":\"count\",\"created_at\":\"2019-12-30 03:01:27\",\"updated_at\":\"2020-01-17 16:49:04\",\"products\":133,\"options\":[{\"name\":\"1000rds\",\"products\":53,\"chosen\":false},{\"name\":\"500rds\",\"products\":44,\"chosen\":false},{\"name\":\"200rds\",\"products\":17,\"chosen\":false},{\"name\":\"600rds\",\"products\":3,\"chosen\":false},{\"name\":\"1,000rds (20 boxes)\",\"products\":2,\"chosen\":false},{\"name\":\"5\",\"products\":2,\"chosen\":false},{\"name\":\"5000rds\",\"products\":2,\"chosen\":false},{\"name\":\"500rds (25 boxes)\",\"products\":2,\"chosen\":false},{\"name\":\"750rds\",\"products\":2,\"chosen\":false},{\"name\":\"2000rds\",\"products\":1,\"chosen\":false},{\"name\":\"250rds\",\"products\":1,\"chosen\":false},{\"name\":\"280rds\",\"products\":1,\"chosen\":false},{\"name\":\"400rds\",\"products\":1,\"chosen\":false},{\"name\":\"500rds,5000rds\",\"products\":1,\"chosen\":false},{\"name\":\"640rds\",\"products\":1,\"chosen\":false}]},{\"id\":650,\"name\":\"Grain\",\"display_name\":\"Grain\",\"handle\":null,\"is_enabled\":1,\"category_id\":30,\"position\":11,\"sort_by\":\"count\",\"created_at\":\"2019-12-30 03:01:27\",\"updated_at\":\"2020-01-17 16:49:04\",\"products\":151,\"options\":[{\"name\":\"180grn\",\"products\":9,\"chosen\":false},{\"name\":\"158grn\",\"products\":8,\"chosen\":false},{\"name\":\"124grn\",\"products\":7,\"chosen\":false},{\"name\":\"145grn\",\"products\":7,\"chosen\":false},{\"name\":\"174grn\",\"products\":7,\"chosen\":false},{\"name\":\"230grn\",\"products\":7,\"chosen\":false},{\"name\":\"115grn\",\"products\":6,\"chosen\":false},{\"name\":\"150grn\",\"products\":6,\"chosen\":false},{\"name\":\"40grn\",\"products\":6,\"chosen\":false},{\"name\":\"125grn\",\"products\":5,\"chosen\":false},{\"name\":\"139grn\",\"products\":5,\"chosen\":false},{\"name\":\"122grn\",\"products\":4,\"chosen\":false},{\"name\":\"130grn\",\"products\":4,\"chosen\":false},{\"name\":\"200grn\",\"products\":4,\"chosen\":false},{\"name\":\"110grn\",\"products\":3,\"chosen\":false},{\"name\":\"123grn\",\"products\":3,\"chosen\":false},{\"name\":\"147grn\",\"products\":3,\"chosen\":false},{\"name\":\"55grn\",\"products\":3,\"chosen\":false},{\"name\":\"94grn\",\"products\":3,\"chosen\":false},{\"name\":\"95grn\",\"products\":3,\"chosen\":false},{\"name\":\"148grn\",\"products\":2,\"chosen\":false},{\"name\":\"165grn\",\"products\":2,\"chosen\":false},{\"name\":\"182grn\",\"products\":2,\"chosen\":false},{\"name\":\"208grn\",\"products\":2,\"chosen\":false},{\"name\":\"60grn\",\"products\":2,\"chosen\":false},{\"name\":\"62grn\",\"products\":2,\"chosen\":false},{\"name\":\"85grn\",\"products\":2,\"chosen\":false},{\"name\":\"93grn\",\"products\":2,\"chosen\":false},{\"name\":\"98grn\",\"products\":2,\"chosen\":false},{\"name\":\"100grn\",\"products\":1,\"chosen\":false},{\"name\":\"108grn\",\"products\":1,\"chosen\":false},{\"name\":\"115grain\",\"products\":1,\"chosen\":false},{\"name\":\"120grn\",\"products\":1,\"chosen\":false},{\"name\":\"124\",\"products\":1,\"chosen\":false},{\"name\":\"140grn\",\"products\":1,\"chosen\":false},{\"name\":\"156grn\",\"products\":1,\"chosen\":false},{\"name\":\"168grn\",\"products\":1,\"chosen\":false},{\"name\":\"170grn\",\"products\":1,\"chosen\":false},{\"name\":\"175grn\",\"products\":1,\"chosen\":false},{\"name\":\"17grn\",\"products\":1,\"chosen\":false},{\"name\":\"180\",\"products\":1,\"chosen\":false},{\"name\":\"185grn\",\"products\":1,\"chosen\":false},{\"name\":\"196grn\",\"products\":1,\"chosen\":false},{\"name\":\"198grn\",\"products\":1,\"chosen\":false},{\"name\":\"210grn\",\"products\":1,\"chosen\":false},{\"name\":\"220grn\",\"products\":1,\"chosen\":false},{\"name\":\"225grn\",\"products\":1,\"chosen\":false},{\"name\":\"240grn\",\"products\":1,\"chosen\":false},{\"name\":\"30grn\",\"products\":1,\"chosen\":false},{\"name\":\"38grn\",\"products\":1,\"chosen\":false},{\"name\":\"405grn\",\"products\":1,\"chosen\":false},{\"name\":\"45grn\",\"products\":1,\"chosen\":false},{\"name\":\"59grn\",\"products\":1,\"chosen\":false},{\"name\":\"625grn\",\"products\":1,\"chosen\":false},{\"name\":\"661grn\",\"products\":1,\"chosen\":false},{\"name\":\"69grn\",\"products\":1,\"chosen\":false},{\"name\":\"73grn\",\"products\":1,\"chosen\":false},{\"name\":\"75grn\",\"products\":1,\"chosen\":false},{\"name\":\"92grn\",\"products\":1,\"chosen\":false}]},{\"id\":653,\"name\":\"Jacket\",\"display_name\":\"Jacket\",\"handle\":null,\"is_enabled\":1,\"category_id\":30,\"position\":12,\"sort_by\":\"count\",\"created_at\":\"2019-12-30 03:01:27\",\"updated_at\":\"2020-01-17 16:49:04\",\"products\":128,\"options\":[{\"name\":\"Copper\",\"products\":108,\"chosen\":false},{\"name\":\"Bi-metal\",\"products\":19,\"chosen\":false},{\"name\":\"Brass\",\"products\":1,\"chosen\":false}]},{\"id\":652,\"name\":\"Core\",\"display_name\":\"Core\",\"handle\":null,\"is_enabled\":1,\"category_id\":30,\"position\":13,\"sort_by\":\"count\",\"created_at\":\"2019-12-30 03:01:27\",\"updated_at\":\"2020-01-17 16:49:04\",\"products\":136,\"options\":[{\"name\":\"Lead\",\"products\":133,\"chosen\":false},{\"name\":\"Steel (SS109)\",\"products\":2,\"chosen\":false},{\"name\":\"Lead-Free\",\"products\":1,\"chosen\":false}]}],\"total\":206,\"category\":{\"parents\":[],\"name\":\"Ammunition\",\"children\":[{\"bc_id\":96,\"name\":\"9mm\",\"url\":\"/ammunition/9mm/\",\"total\":18},{\"bc_id\":131,\"name\":\".45 Auto\",\"url\":\"/ammunition/45-auto/\",\"total\":12},{\"bc_id\":74,\"name\":\".22 LR\",\"url\":\"/ammunition/22-lr/\",\"total\":12},{\"bc_id\":90,\"name\":\".308 Winchester\",\"url\":\"/ammunition/308-winchester/\",\"total\":10},{\"bc_id\":129,\"name\":\".40 S&W\",\"url\":\"/ammunition/40-s-w/\",\"total\":9},{\"bc_id\":107,\"name\":\"7.62x39\",\"url\":\"/ammunition/7-62x39/\",\"total\":9},{\"bc_id\":190,\"name\":\"Shotgun\",\"url\":\"/ammunition/shotgun/\",\"total\":7},{\"bc_id\":126,\"name\":\".380 Auto\",\"url\":\"/ammunition/380-auto/\",\"total\":7},{\"bc_id\":121,\"name\":\".38 Special\",\"url\":\"/ammunition/38-special/\",\"total\":7},{\"bc_id\":214,\"name\":\"7.62x54R\",\"url\":\"/ammunition/7-62x54r/\",\"total\":7},{\"bc_id\":125,\"name\":\"10mm\",\"url\":\"/ammunition/10mm/\",\"total\":6},{\"bc_id\":85,\"name\":\".30-06\",\"url\":\"/ammunition/30-06/\",\"total\":6},{\"bc_id\":78,\"name\":\".223 Remington\",\"url\":\"/ammunition/223-remington/\",\"total\":5},{\"bc_id\":106,\"name\":\"7.62x51\",\"url\":\"/ammunition/7-62x51/\",\"total\":5},{\"bc_id\":122,\"name\":\"5.45x39\",\"url\":\"/ammunition/5-45x39/\",\"total\":4},{\"bc_id\":89,\"name\":\".303 British\",\"url\":\"/ammunition/303-british/\",\"total\":4},{\"bc_id\":87,\"name\":\".300 AAC Blackout\",\"url\":\"/ammunition/300-aac-blackout/\",\"total\":4},{\"bc_id\":98,\"name\":\"8mm Mauser\",\"url\":\"/ammunition/8mm-mauser/\",\"total\":4},{\"bc_id\":94,\"name\":\"9x18 Makarov\",\"url\":\"/ammunition/9x18-makarov/\",\"total\":4},{\"bc_id\":91,\"name\":\".32 Auto\",\"url\":\"/ammunition/32-auto/\",\"total\":3},{\"bc_id\":118,\"name\":\"6.5 Grendel\",\"url\":\"/ammunition/6-5-grendel/\",\"total\":3},{\"bc_id\":112,\"name\":\".357 Magnum\",\"url\":\"/ammunition/357-magnum/\",\"total\":3},{\"bc_id\":124,\"name\":\"5.56x45\",\"url\":\"/ammunition/5-56x45/\",\"total\":3},{\"bc_id\":191,\"name\":\".22 WMR\",\"url\":\"/ammunition/22-wmr/\",\"total\":3},{\"bc_id\":130,\"name\":\".44 Magnum\",\"url\":\"/ammunition/44-magnum/\",\"total\":3},{\"bc_id\":113,\"name\":\"6.8 Remington SPC\",\"url\":\"/ammunition/6-8-remington-spc/\",\"total\":2},{\"bc_id\":110,\"name\":\"7.5 Swiss\",\"url\":\"/ammunition/7-5-swiss/\",\"total\":2},{\"bc_id\":108,\"name\":\"7.62x25 Tokarev\",\"url\":\"/ammunition/7-62x25-tokarev/\",\"total\":2},{\"bc_id\":117,\"name\":\"6.5x55 Swede\",\"url\":\"/ammunition/6-5x55-swede/\",\"total\":2},{\"bc_id\":120,\"name\":\"6.5 Carcano\",\"url\":\"/ammunition/6-5-carcano/\",\"total\":2},{\"bc_id\":247,\"name\":\"8x56R\",\"url\":\"/ammunition/8x56r/\",\"total\":2},{\"bc_id\":210,\"name\":\".50 BMG\",\"url\":\"/ammunition/50-bmg/\",\"total\":2},{\"bc_id\":72,\"name\":\".17 HMR\",\"url\":\"/ammunition/17-hmr/\",\"total\":2},{\"bc_id\":84,\"name\":\".30 Carbine\",\"url\":\"/ammunition/30-carbine/\",\"total\":2},{\"bc_id\":123,\"name\":\".38 Super\",\"url\":\"/ammunition/38-super/\",\"total\":2},{\"bc_id\":114,\"name\":\".357 Sig\",\"url\":\"/ammunition/357-sig/\",\"total\":2},{\"bc_id\":86,\"name\":\".30-30 Winchester\",\"url\":\"/ammunition/30-30-winchester/\",\"total\":2},{\"bc_id\":73,\"name\":\".22 Hornet\",\"url\":\"/ammunition/22-hornet/\",\"total\":1},{\"bc_id\":266,\"name\":\"7.7\u00d758 Japanese Arisaka\",\"url\":\"/ammunition/7-7-58-japanese-arisaka/\",\"total\":1},{\"bc_id\":102,\"name\":\"7.65 Para\",\"url\":\"/ammunition/7-65-para/\",\"total\":1},{\"bc_id\":101,\"name\":\"7mm Mauser\",\"url\":\"/ammunition/7mm-mauser/\",\"total\":1},{\"bc_id\":100,\"name\":\"7mm Remington Magnum\",\"url\":\"/ammunition/7mm-remington-magnum/\",\"total\":1},{\"bc_id\":80,\"name\":\".25 Auto\",\"url\":\"/ammunition/25-auto/\",\"total\":1},{\"bc_id\":99,\"name\":\"8mm Kurz\",\"url\":\"/ammunition/8mm-kurz/\",\"total\":1},{\"bc_id\":104,\"name\":\"7.63 Mauser\",\"url\":\"/ammunition/7-63-mauser/\",\"total\":1},{\"bc_id\":95,\"name\":\"8x50R Lebel\",\"url\":\"/ammunition/8x50r-lebel/\",\"total\":1},{\"bc_id\":79,\"name\":\".243 Winchester\",\"url\":\"/ammunition/243-winchester/\",\"total\":1},{\"bc_id\":77,\"name\":\".222 Remington\",\"url\":\"/ammunition/222-remington/\",\"total\":1},{\"bc_id\":76,\"name\":\".22-250\",\"url\":\"/ammunition/22-250/\",\"total\":1},{\"bc_id\":75,\"name\":\".22 Remington Jet Magnum\",\"url\":\"/ammunition/22-remington-jet-magnum/\",\"total\":1},{\"bc_id\":103,\"name\":\"7.65 Argentine\",\"url\":\"/ammunition/7-65-argentine/\",\"total\":1},{\"bc_id\":83,\"name\":\".270 Winchester\",\"url\":\"/ammunition/270-winchester/\",\"total\":1},{\"bc_id\":81,\"name\":\".25-06\",\"url\":\"/ammunition/25-06/\",\"total\":1},{\"bc_id\":82,\"name\":\".264 Winchester Magnum\",\"url\":\"/ammunition/264-winchester-magnum/\",\"total\":1},{\"bc_id\":128,\"name\":\".45 Long Colt\",\"url\":\"/ammunition/45-long-colt/\",\"total\":1},{\"bc_id\":109,\"name\":\"7.62 Nagant\",\"url\":\"/ammunition/7-62-nagant/\",\"total\":1},{\"bc_id\":200,\"name\":\"7.5 French\",\"url\":\"/ammunition/7-5-french/\",\"total\":1},{\"bc_id\":88,\"name\":\".300 Winchester Magnum\",\"url\":\"/ammunition/300-winchester-magnum/\",\"total\":1},{\"bc_id\":92,\"name\":\".32 S&W Long\",\"url\":\"/ammunition/32-s-w-long/\",\"total\":1},{\"bc_id\":265,\"name\":\"6.5\u00d750 Japanese Arisaka\",\"url\":\"/ammunition/6-5-50-japanese-arisaka/\",\"total\":1},{\"bc_id\":93,\"name\":\".338 Lapua\",\"url\":\"/ammunition/338-lapua/\",\"total\":1},{\"bc_id\":119,\"name\":\"6.5 Creedmoor\",\"url\":\"/ammunition/6-5-creedmoor/\",\"total\":1},{\"bc_id\":116,\"name\":\".38 S&W\",\"url\":\"/ammunition/38-s-w/\",\"total\":1},{\"bc_id\":192,\"name\":\".44 Special\",\"url\":\"/ammunition/44-special/\",\"total\":1},{\"bc_id\":127,\"name\":\".45-70\",\"url\":\"/ammunition/45-70/\",\"total\":1},{\"bc_id\":198,\"name\":\".45 GAP\",\"url\":\"/ammunition/45-gap/\",\"total\":1}]}}");
        //    var productService = new ProductService(mockedHttp.ToHttpClient(), );

        //    // Act

        //    // Assert
        //}
    }
}
