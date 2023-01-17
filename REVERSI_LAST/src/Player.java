/**
 * Игрок.
 */
public class Player {

    /**
     * Имя игрока.
     */
    String name;

    /**
     * Лучший результат игрока.
     */
    int bestResult = -1;

    /**
     * Текущий счет игрока.
     */
    int currentResult = -1;

    /**
     * Создание игрока.
     * @param name Имя игрока.
     */
    public Player(String name) {
        this.name = name;
    }

    /**
     * Получение имени игрока.
     * @return Имя игрока.
     */
    public String GetName() {
        return name;
    }

    /**
     * Записывание имени игрока.
     * @param name Имя игрока.
     */
    public void setName(String name) {
        this.name = name;
    }

    /**
     * Возвращает лучший результат игрока.
     * @return Максимальное количество очков, набранных игроком.
     */
    public String showResult() {
        if (bestResult == -1) {
            return name + " ещё не играл(а).";
        }
        return name + ": " + bestResult;
    }

    /**
     * Изменение лучшего результата.
     * @param result Новый результат.
     */
    public void setBestResult(int result) {
        if (result > bestResult) {
            this.bestResult = result;
        }
    }

    /**
     * Выводит лучшие результат игры.
     */
    public void showBestResult() {
        if (bestResult > -1) {
            System.out.println("Лучший результат игрока " + name +
                    " = " + bestResult);
        }
        else {
            System.out.println(name + " еще не играл(а).");
        }
    }
}
