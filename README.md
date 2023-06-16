Як користуватися:
1. Заповнення  `Dictionary<Ingredient, uint> _ingredientStorage;` через  `PizzeriaData.Storage.PutIngredient( new Ingredient() { Name = "Cheese" }, 100);` або хардкодом (як зараз).
2. Створення інгредієнтів, рецептів, додавання рецептів до відповідних кухарів.
3. Додавання кухарів через `ChefManager` (треба це пофіксити, бо треба додавати через адміна).
4. Додавати замовлення типу
``` charp
PizzeriaData.ChefManager.AddOrder("Pizza");
PizzeriaData.ChefManager.AddOrder("Cake");
```
Нічого запускати в `ChefManager` не потрібно, окремий потік виділяється при його створенні, при додаванні замовлень менеджеру автоматично розподіляється між кухарями через безкінечний цикл.
