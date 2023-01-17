/**
 * Ход в игре.
 */
public class Move {

    /**
     * Размер игрового поля.
     */
    int fieldSize;

    /**
     * Х координата хода.
     */
    int x;

    /**
     * Y координата хода.
     */
    int y;

    /**
     * Возможен ли ход.
     */
    boolean isPossible;

    /**
     * Создание хода по координатам и размерам поля.
     * @param x Х координата хода.
     * @param y Y координата хода.
     * @param fieldSize Размер игрового поля.
     */
    public Move(int x, int y, int fieldSize) {
        this.fieldSize = fieldSize;
        try {
            setX(x);
            setY(y);
        }
        catch (Exception e) {
            isPossible = false;
            System.out.println(e.getMessage());
        }
        isPossible = true;
    }

    /**
     * Создание хода без координат (по умолчанию невозможен)
     * @param fieldSize Размер игрового поля.
     */
    public Move(int fieldSize) {
        this.fieldSize = fieldSize;
        isPossible = false;
    }

    /**
     * Заполнение X координаты.
     * @param x Х координата хода.
     * @throws Exception Если координата вне поля.
     */
    public void setX(int x) throws Exception {
        if (x < 0 || x >= fieldSize) {
            throw new Exception("Координата вне поля");
        }
        isPossible = true;
        this.x = x;
    }

    /**
     * Возвращает Х координату хода.
     * @return Х координату хода.
     */
    public int getx() {

        return this.x;
    }

    /**
     * Заполнение Y координаты.
     * @param y Y координата хода.
     * @throws Exception Если координата вне поля.
     */
    public void setY(int y) throws Exception {
        if (y < 0 || y > fieldSize) {
            throw new Exception("Координата вне поля");
        }
        this.y = y;
    }

    /**
     * Возвращает Y координату хода.
     * @return Y координату хода.
     */
    public int getY() {
        return this.y;
    }

    /**
     * Возвращает информацию о возможности хода.
     * @return Возможен ли ход.
     */
    public boolean getPossibility() {
        return isPossible;
    }
}
