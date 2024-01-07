# 💰 Bankovní systém MyBank 💰

[![forthebadge](https://forthebadge.com/images/badges/fuck-it-ship-it.svg)](https://forthebadge.com)
[![forthebadge](https://forthebadge.com/images/badges/built-with-love.svg)](https://forthebadge.com)

## O aplikaci ##

> - #### Aplikace je rozdělena na 3 části, webová část, bankomat a library
> - #### Ve webové části je udělána API služba pro jednoduchou komunikaci s bankomatem

## Bankomat ##

> - #### Napsáno ve Windows Forms
> - #### 2 tlačítka, jedno pro vybrání peněz, druhé pro vložení
> - #### Textbox pro zadání částky
> - #### Textbox pro zadání čísla účtu
> - #### Komunikace s DB probírá přes API

## Transakce ##
> - #### Je možné provádět pouze pokud máte propojenou kartu
> - #### Lze vidět seznam vašich transakcí
> - #### Veškeré transakce se zapisují do DB

## Admin menu ##
> - #### Po přihlášení lze vidět v navbaru admin menu
> - #### Do tohoto menu se lze dostat pouze, pokud jste označení jako admin
> - #### Lze tam např. přidávat a odebírát karty, vytvářet a mazat klienty
> - #### Zároveň pokud jste admin, můžete generovat xlsx soubory
