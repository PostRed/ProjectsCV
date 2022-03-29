# -*- coding: utf-8 -*-

import sys
import random
from PySide2.QtCore import *
from PySide2.QtWidgets import *


def count_dif(num):
    num = str(num)
    num_set = set()
    for i in range(len(num)):
        num_set.add(num[i])
    return len(num_set)


class bulls_and_cows(QObject):
    game_process = Signal(str)


    def __init__(self, ans = str, text = str):
        QObject.__init__(self)
        self.__ans = random.randint(1000, 9999)            
        while count_dif(self.__ans) != 4:
            self.__ans = random.randint(1000, 9999) 
        print(self.__ans) 
        self.__data = ""
        self.__FINISH = "loss"
        self.__prevNum = ""
        self.__attemps = 0
        
        
    def setValue(self, value):
        self.__data = value      
    
    
    def finish(self):
        if self.__FINISH == "loss":
            return self.loss()
        elif self.__FINISH == "new_game":
            return self.NewGame()
    
    def ans_game(self):
        if cor_chek(self.__data):
            if str(self.__data) == str(self.__ans):
                self.game_process.emit(self.Victory())
            else:
                if self.__data != self.__prevNum:
                    self.__attemps += 1
                self.game_process.emit(number_input(str(self.__data), str(self.__ans)))
            self.__prevNum = self.__data
        else:
            self.game_process.emit("число некорректно")
            
            
    def NewGame(self):
        self.__ans = random.randint(1000, 9999)            
        while count_dif(self.__ans) != 4:
            self.__ans = random.randint(1000, 9999) 
        print(self.__ans) 
        self.__data = ""
        self.__FINISH = "loss"
        self.__prevNum = ""
        self.__attemps = 0
        LineEdit.setText("")
        LineEdit.setEnabled(True)
        Check.setEnabled(True)
        Check.setText("УГАДАТЬ")
        res_text.setText("")
        New_game.setEnabled(True)
        New_game.setText("СДАТЬСЯ")
        
        
    def Victory(self):
        LineEdit.setEnabled(False)
        Check.setEnabled(False)
        self.__attemps += 1
        self.game_process.emit("ЭТО ПОБЕДА!!! Вы угадали число и справились с этим за " + str(self.__attemps) + " попыток!")
        New_game.setText("НОВАЯ ИГРА")
        self.__FINISH = "new_game"
        return "ЭТО ПОБЕДА!!! Вы угадали число и справились с этим за " + str(self.__attemps) + " попыток!"

        
    def loss(self):
        Check.setEnabled(False)
        self.__attemps += 1
        self.game_process.emit("Вы не справились с задачей, хотя вам требовалось всего лишь угадать это - " + str(self.__ans))
        New_game.setText("НОВАЯ ИГРА")
        self.__FINISH = "new_game"
        return "Вы не справились с задачей, хотя вам требовалось всего лишь угадать это - " + str(self.__ans)
            
            
def cor_chek(n):
    return count_dif(n) == 4 and n.isdigit() and str(n)[0] != '0'
        
    
def number_input(text, ans):
    bulls = 0
    cows = 0
    for i in range(4):
        if text[i] == ans[i]:
            bulls += 1
        elif text[i] in ans:
            cows += 1
    return "быки: " + str(bulls) + "  коровы: " + str(cows)


app = QApplication(sys.argv)
Window = QMainWindow()
Window.resize(500, 500)
LineEdit = QLineEdit(Window)
LineEdit.setGeometry(10, 50, 110, 30)

user_game = bulls_and_cows()

Check = QPushButton('УГАДАТЬ', Window)
Check.setGeometry(10, 10, 110, 30)

res_text = QLabel(Window)
res_text.setGeometry(10, 90, 500, 30)

New_game = QPushButton('СДАТЬСЯ', Window)
New_game.setGeometry(10, 130, 110, 30)

user_game.game_process.connect(res_text.setText)
LineEdit.textChanged.connect(user_game.setValue)
Check.clicked.connect(user_game.ans_game)
New_game.clicked.connect(user_game.finish)

Window.show()
app.exec_()