# Task2

## Academy'18 • 2nd stage • 2. C# Modern Features

Необходимо реализовать консольное приложение с пользовательским вводом (меню, форматированный вывод и т.д.), эмулирующее работу парковки.
### Программа должна иметь следующий функционал:
- Добавить/удалить машину с парковки.
- Пополнить баланс машины.
- Списать средства за парковочное место (через каждые N-секунд будет срабатывать таймер и списывать с каждой машины стоимость парковки).
- Вывести истории транзакций за последнюю минуту.
- Вывести общий доход парковки.
- Вывести количество свободных мест на парковке.
- Каждую минуту записывать в файл Transactions.log сумму транзакций за последнюю минуту с пометкой даты.
- Вывести Transactions.log (отформатировать вывод)

**Общая логика**: В программе должна быть только 1 парковка. При инициализации парковки необходимо использовать класс Settings. Парковка хранит список машин/транзакций.

Можно добавить машину на парковку либо забрать с парковки. Каждые N-секунд парковка списывает средства у машины. Если у машины недостаточно средств на оплату парковки, то списывать штраф (коэф. штрафа * цена за парковку). Также мы не можем забрать машину, пока не пополним баланс и не повторим операцию.

При списывании средств, парковка хранит транзакции за последнюю минуту. В любой момент времени, мы можем обратиться к парковке и узнать текущий баланс (заработанные средства). Мы можем обратиться к парковке и узнать сумму заработанных средств за последнюю минуту. Мы можем обратиться к парковке и узнать кол-во свободных/кол-во занятых мест на парковке. Стоимость парковки зависит от типа машины.

**Список классов (без методов), которые можно использовать в программе:**

### Settings (read-only / static class):

Свойство Timeout (каждые N-секунд списывает средства за парковочное место) - по умолчанию 3 секунды

Dictionary - словарь для хранения цен за парковку (например: для грузовых - 5, для легковых - 3, для автобусов - 2, для мотоциклов - 1)

Свойство ParkingSpace - вместимость парковки (общее кол-во мест)

Свойство Fine - коэффициент штрафа

### Parking - данный класс при инициализации использует настройки описанные в классе Settings:

Список машин

Список транзакций

Свойство Баланс (заработанные средства)

### Car:

Свойство идентификатор

Свойство баланс

Свойство тип машины

### CarType:

 Passenger
 
 Truck
 
 Bus
 
 Motorcycle

### Transaction

Свойство Дата/Время Транзакции

Свойство Идентификатор машины

Свойство Списанные средства

### Menu
Реализовать простое меню навигации по программе
Примечания:
-    Использовать паттерн Singleton
-    Предусмотреть обработку исключений
-    Следить за чистотой кода