#! /usr/bin/env python
# -*- coding: utf-8 -*-
import requests
from bs4 import BeautifulSoup
import time
import smtplib


class Currency:
    DOLLAR_GRN = "https://www.google.com/search?client=opera&q=долар+в+грн&sourceid=opera&ie=UTF-8&oe=UTF-8"
    headers = {
        'User-Agent': 'Mozilla/5.0 (Macintosh; Intel Mac OS X 10_15_3) Applewebkit/537.36 (KHTML like Gecko) Chrome/80.0.3987.149 Safari/537.36'}
    current_converted_price = 0
    difference = 5

    def init(self):
        self.current_converted_price = float(self.get_currency_price().replace(",", "."))

    def get_currency_price(self):
        full_page = requests.get(self.DOLLAR_GRN, headers=self.headers)

        soup = BeautifulSoup(full_page.content, 'html.parser')
        convert = soup.findAll("span", {"class": "DFlfde SwHCTb", "data-precision": 2})
        return convert[0].text


    def check_currency(self):
        currency = float(self.get_currency_price().replace(",", "."))
        if currency >= self.current_converted_price + self.difference:
            print("Курс дуже піднявся, можливо потрібно щось робити?")
            self.send_mail()
        elif currency <= self.current_converted_price - self.difference:
            print("Курс дуже впав, можливо потрібно щось робити")
            self.send_mail()
        print("Зараз курс: 1 доллар = " + str(currency))
        time.sleep(3)
        self.check_currency()

    def send_mail(self):
        server = smtplib.SMTP('smtp.gmail.com', 587)
        # server.ehlo()
        # server.starttls()
        # server.ehlo()
        # server.login('ВАША ПОШТА', 'ПАРОЛЬ')
        # subject = 'Currency mail'
        # body = 'Currency has been changed!'
        # message = f'Subiect: {subject}\n{body}'
        #
        # server.sendmail(
        #     'От кого',
        #     'Кому',
        #     message
        # )
        server.quit()



currency = Currency()
currency.check_currency()