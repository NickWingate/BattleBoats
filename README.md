# Battle Boats
Battle ships in WPF!

## Success Criteria
#### Programming practices:
1. Loosely couple the UI/design from the business logic
   + Use the MVVM design pattern (Model, View, ViewModel)
   + Makes testing easier
2. Use dependency injection to remove concrete dependencies between classes
3. Use SRP(single responsibility principle) to avoid confusing methods and classes that do more than required
4. Use resources in XAML making it easier to change the design

#### User Experience:
1. Have a intuitive main menu for the user
    + Big buttons, clear font
    + Animations on hover
    + Not too many options or buttons
2. Save game state to file after every move
   + Save as json file
3. Create or implement the most efficient method for the computer to win
4. Make placing and rotating ships easy and not cumbersome e.g. the easiest would be drag and drop, most cumbersome would be entering the coordinates of the start of the boat
5. If i have time create a settings/config file where the user can define the difficulty, grid size, boat types etc.
