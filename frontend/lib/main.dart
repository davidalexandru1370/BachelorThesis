import 'package:camera/camera.dart';
import 'package:flutter/material.dart';
import 'package:frontend/screens/login_screen.dart';
import 'package:frontend/screens/main_page.dart';
import 'package:frontend/widgets/navigation_bar.dart';
import 'package:provider/provider.dart';
import 'package:flutter_gen/gen_l10n/app_localizations.dart';

Future<void> main() async {
  var firstCamera = await ensureCameraWorks();

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
            builder: (context, localeModel, child) =>
                MaterialApp(
                    title: 'Flutter Demo',
                    theme: ThemeData(
                      colorScheme: ColorScheme.fromSeed(
                          seedColor: const Color.fromARGB(255, 119, 119, 119)),
                      useMaterial3: true,
                    ),
                    supportedLocales: AppLocalizations.supportedLocales,
                    locale: localeModel.locale,
                    localizationsDelegates: AppLocalizations
                        .localizationsDelegates,
                    home: ApplicationNavigationBar())));
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

Future<CameraDescription> ensureCameraWorks() async {
  WidgetsFlutterBinding.ensureInitialized();

  final cameras = await availableCameras();

  final firstCamera = cameras.first;

  return firstCamera;
}