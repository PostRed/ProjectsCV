/**
 * Игра с двумя игроками.
 */
public class GameForTwo extends Game {

    /**
     * первый игрок.
     */
    Player player1;

    /**
     * Второй игрок.
     */
    Player player2;

    /**
     * Создание игры с двумя игроками.
     * @param player1 Первый игрок.
     * @param player2 Второй игрок.
     */
    public GameForTwo(Player player1, Player player2) {
        this.player1 = player1;
        this.player2 = player2;
    }

    /**
     * Вывод результата игры.
     */
    public void showResults() {
        int result1 = getResult('*');
        player1.setBestResult(result1);
        int result2 = getResult('&');
        player2.setBestResult(result2);
        if (result1 > result2) {
            System.out.println("Победил(а) " + player1.name + "!");
            System.out.println("Со счетом:  " + result1 + ":" + result2);
        }
        else {
            System.out.println("Победил(а) " + player2.name + "!");
            System.out.println("Со счетом:  " + result2 + ":" + result1);
        }
    }

    /**
     * Запускает игру и вызывает метод, отвечающий за ходы игроков.
     */
    public void startGameProcess() {
        while (gameIsPossible()) {
            showField();
            if (playerSymbol == '*') {
                System.out.println(player1.name + ", Ваш ход!");
            }
            else {
                System.out.println(player2.name + ", Ваш ход!");
            }
            playerMove();
        }
        showResults();
    }
}
