import 'package:flutter/material.dart';
import 'package:frontend/screens/main_page.dart';

class ApplicationNavigationBar extends StatefulWidget {
  const ApplicationNavigationBar({Key? key}) : super(key: key);

  @override
  State<StatefulWidget> createState() => _ApplicationNavigationBarState();
}

class _ApplicationNavigationBarState extends State<ApplicationNavigationBar> {
  int currentPageIndex = 0;

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      bottomNavigationBar: NavigationBar(
        onDestinationSelected: (int index) {
          setState(() {
            currentPageIndex = index;
          });
        },
        indicatorColor: Colors.amber[800],
        selectedIndex: currentPageIndex,
        destinations: const <Widget>[
          NavigationDestination(
            icon: Icon(Icons.home),
            selectedIcon: Icon(Icons.home),
            label: 'Home',
          ),
          NavigationDestination(
            icon: Icon(Icons.search),
            selectedIcon: Icon(Icons.search),
            label: 'Search',
          ),
          NavigationDestination(
            icon: Icon(Icons.person_outline),
            selectedIcon: Icon(Icons.person),
            label: 'Profile',
          ),
        ],
      ),
      body: <Widget>[Container(child: MainPage())][currentPageIndex],
    );
  }
}
