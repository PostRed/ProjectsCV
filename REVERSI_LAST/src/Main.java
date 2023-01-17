import java.util.ArrayList;
import java.util.Scanner;
import java.util.List;

/**
 * Выбор функции и запуск игры.
 */
public class Main {

    /**
     * игроки, принимающие участие (имена должны быть уникальны).
     */
    static List<Player> players = new ArrayList<Player>();

    /**
     * Количество функций в меню.
     */
    static int countOfMenuItems = 4;

    /**
     * Считывание числа, при неверном вводе -
     * сообщение об ошибке и новое считывание
     * @return Введенное число.
     */
    public static int readInt() {
        Scanner in = new Scanner(System.in);
        boolean correct = false;
        int result = 0;
        while (!correct) {
            try {
                var input = in.nextLine();
                result = Integer.parseInt(input);
                correct = true;
            }
            catch (Exception e) {
                System.out.println("Ошибка ввода, давайте еще раз:");
            }
        }
        return result;
    }

    /**
     * Считывание имени и создание игрока.
     * @param mainPlayer Основной ли игрок (или его соперник).
     * @return Созданного игрока.
     */
    public static Player getPlayer(boolean mainPlayer) {
        Scanner in = new Scanner(System.in);
        if (mainPlayer) {
            System.out.println("Как Вас зовут?:");
        }
        else {
            System.out.println("Введите имя Вашего соперника:" );
        }
        String name = in.nextLine();
        for (Player player : players) {
            if (player.GetName().equals(name)) {
                return player;
            }
        }
        Player newPlayer = new Player(name);
        players.add(newPlayer);
        return newPlayer;
    }

    /**
     * Показывает меню.
     */
    public static void showMenu() {
        System.out.println("Выберите номер команды: \n" +
                "1 - Игра с компьютером\n" +
                "2 - Игра с другом\n" +
                "3 - Показать лучшие результаты\n" +
                "4 - Выход");
    }

    /**
     * Вывод лучших результатов игры.
     */
    public static void showBestResults() {
        if (players.size() == 0) {
            System.out.println("Еще никто не играл.");
        }
        else {
            for (Player player : players) {
                player.showBestResult();
            }
        }
    }

    /**
     * Запуск игры для двоих.
     */
    public static void runGameForTwo() {
        Player player2 = getPlayer(false);
        System.out.println(players.get(0).name + ", Вы ходите таким символом - *");
        System.out.println(player2.name + ", Вы ходите таким символом - &");
        GameForTwo game = new GameForTwo(players.get(0), player2);
        game.startGameProcess();
    }

    /**
     * Запуск игры с компьютером.
     */
    public static void runComputerGame() {
        System.out.println(players.get(0).name + ", Вы ходите таким символом - *");
        System.out.println("Компьютер ходит ходите таким символом - &");
        ComputerGame game = new ComputerGame(players.get(0));
        game.startGameProcess();
    }

    /**
     * Выполняет функцию, выбранную из меню.
     * @param menuItem Функция меню.
     */
    public static void executeMenuitem(int menuItem) {
        if (menuItem == 2) {
           runGameForTwo();
        }
        else if (menuItem == 3) {
            showBestResults();
        }
        else if (menuItem == 1) {
            runComputerGame();
        }
    }

    /**
     * Выбор функции меню.
     * @return Является ли выбранная функция выходом из игры.
     */
    public static boolean menuItemSelection() {
        showMenu();
        int menuItem = readInt();
        int exitItem = 4;
        while (menuItem < 1 || menuItem > countOfMenuItems) {
            System.out.println("Неверный номер команды! Давайте еще раз:");
            menuItem = readInt();
        }
        if (menuItem == exitItem) {
            return false;
        }
        executeMenuitem(menuItem);
        return true;
    }

    /**
     * Запускает программу для игры.
     */
    public static void startGame() {
        System.out.println("Здравствуйте! Добро пожаловать в игру REVERSI!");
        getPlayer(true);
        while (menuItemSelection()) {
            continue;
        }
        System.out.println("До новых встреч :)");
    }

    /**
     * Вызывает метод, стартующий программу.
     * @param args Аргументы.
     */
    public static void main(String[] args) {
        startGame();
    }
}

