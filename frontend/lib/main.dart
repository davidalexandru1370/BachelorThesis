import 'package:flutter/material.dart';
import 'package:frontend/screens/login_screen.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';
import 'package:provider/provider.dart';

void main() {
  runApp(const MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({super.key});

  @override
  State<StatefulWidget> createState() => _MyApp();
}

class _MyApp extends State<MyApp> {

  @override
  Widget build(BuildContext context) {
    return ChangeNotifierProvider(
        create: (context) => LocaleModel(),
        child: Consumer<LocaleModel>(
            builder: (context, localeModel, child) => MaterialApp(
                title: 'Flutter Demo',
                theme: ThemeData(
                  colorScheme: ColorScheme.fromSeed(
                      seedColor: const Color.fromARGB(255, 119, 119, 119)),
                  useMaterial3: true,
                ),
                supportedLocales: AppLocalizations.supportedLocales,
                locale: localeModel.locale,
                localizationsDelegates: AppLocalizations.localizationsDelegates,
                home: const LoginScreen())));
  }
}

class LocaleModel extends ChangeNotifier {
  Locale? _locale;

  Locale? get locale => _locale;

  void setLocale(Locale value) {
    _locale = value;
    notifyListeners();
  }
}
