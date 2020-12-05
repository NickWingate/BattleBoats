# Battle Boats
Battle ships in WPF!

## Success Criteria
#### Programming practices:
+ Loosely couple the UI/design from the business logic
 + Use the MVVM design pattern (Model, View, ViewModel)
+ Use dependency injection to remove concrete dependencies between classes
+ Use SRP(single responsibility principle) to avoid confusing methods and classes that do more than required
+ Use resources in XAML making it easier to change the design

#### User Experience:
+ Have a intuitive main menu for the user
  + Big buttons, clear font
+ Save game state to file after every move
+ Create or implement the most efficient method for the computer to win
+ Make placing and rotating ships easy and not cumbersome e.g. the easiest would be drag and drop, most cumbersome would be entering the coordinates of the start of the boat
+ If i have time create a settings/config file where the user can define the difficulty, grid size, boat types etc.
