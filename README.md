  
# :boat: Model Boat Club Simulator :video_game:  
## Le meilleur simulateur de bateaux télécommandés qu'on peut imaginer ! Enfin presque...  
  
Voici une idée de projet que j'ai commencé ce weekend.  
Je me suis inspiré de [ce projet](https://github.com/RafaelKuebler/Flocking) pour la base de code.  
J'ai commencé par importer des modèles 3D de bateaux.  

![Preview Model Boat Club Simulator](this-is.gif)

J'ai créé 2~3 [Prefab](https://docs.unity3d.com/6000.2/Documentation/Manual/Prefabs.html) mais je me retrouve face à plusieurs problèmes et j'aurais besoin de ton coup de mains !  
  
**:information_source: Tu peux déjà commencer par te créer une version de mon projet en [forkant](https://docs.github.com/en/pull-requests/collaborating-with-pull-requests/working-with-forks/fork-a-repo?tool=webui#forking-a-repository) 👉 [mon repo](https://github.com/romainPechot/Model-Boat-Club-Simulator).**  
**:information_source: Pourrais-tu `commit` à chaque fois que tu a complété une étape et tu m'enverra le lien de ton repo github une fois fini ?**  
**:information_source: Si tu peux mettre un commentaire à chaque commit et à chaque "ligne/bloc" de code histoire que je ne sois pas perdu?**  
**:information_source: Si tu peux modifier seulement les lignes de code que tu estime nécessaire ? Pas de gros copier/coller venant d'internet s'il te plait :expressionless: sauf si tu sais ce que tu fais (et que tu me l'explique en commentaire).**  
  
## 1. Pouvoir éditer pérennement le comportement des bateaux en mode Play  
  
Une fois que la simulation est lancée (en cliquant sur le bouton :arrow_forward:) je ne peux plus éditer les valeurs de comportement de mes bateaux à moins de tous les sélectionner directement dans la scène et il ne faut pas que j'oublie de me rappeler les valeurs sinon elles ne sont pas enregistrées quand je quitte le mode Play !  
  
J'ai commencé à déplacer certaines variables dans le `BoatManager.cs` : ça résout une partie du problème (je n'ai plus besoin de sélectionner tout les bateaux) mais ça n'est pas sauvegardé, ça va être l'horreur pour créer plusieurs variantes et ça n'est pas à lui de gérer/connaitre ses valeurs dans le principe de responsabilité.  
  
**Est-ce que tu pourrais trouver un moyen "d'extraire/externaliser" les variables du script `BoatAutoPilot.cs` ?**  
  
Je crois qu'on peut utiliser les [ScriptableObject](https://docs.unity3d.com/6000.2/Documentation/Manual/class-ScriptableObject.html) pour ça ?  
  
Peut importe la solution que tu utilise tant que je peux :  
- - [ ] Faire mes modifications sur leur comportement en mode Play et que ça reste sauvegardé **automatiquement**, que je quitte le mode Play ou non.  
- - [ ] Que toutes les instances de bateau de la même famille (e.g. "Boat Tow A" et Boat Tow B" font parties de la famille "Tow") aient **le même comportement**. Si je change une valeur du comportement toutes les instances qui référencent ce comportement seront affectées.   
  
**:information_source: Une fois que c'est fait : `commit`, `push` et passe à la suite :smile:**  
  
## 1. (Bonus) Problème inverse (potentiel futur) ?  
  
Si on "retire" les variables du script on ne pourra plus les éditer en sélectionnant directement un bateau dans la scène ?  
En imaginant que j'ai pleins de bateaux différents avec chacun leur variables/comportement unique ça risque de nous faire faire beaucoup d'aller/retour entre le script et l'endroit où se trouve ces variable (plusieurs cliques supplémentaire par bateau, l'horreur pour éditer les données) !  
  
- [ ] Tu pense pouvoir modifier l'éditeur (l'endroit où sont affichées nos variables) afin de les "faire revenir" **visuellement** dans le contexte d'édition (c.a.d. là ou elles étaient à la base, au niveau de notre script `BoatAutoPilot.cs`) ?  
  
J'ai entendu dire que l'inspecteur de Unity est modifiable, notamment via [CustomEditor](https://docs.unity3d.com/6000.2/Documentation/Manual/editor-CustomEditors.html), [SerializedObject](https://docs.unity3d.com/ScriptReference/SerializedProperty.html) et [SerializedProperty](https://docs.unity3d.com/ScriptReference/SerializedProperty.html).  
  
:construction: **C'est beaucoup demander à un profil designer donc n'hésite pas à faire ça en dernier.**  
  
**:information_source: Une fois que c'est fait : `commit`, `push` et passe à la suite :smile:**  
  
## 2. Ils sont bien sympa avec leur Prefab mais...  
  
J'ai commencé a créer des [Prefab](https://docs.unity3d.com/6000.2/Documentation/Manual/Prefabs.html) pour les bateaux mais j'ai plusieurs problèmes.  
  
### :information_source: Dans l'idée j'aimerais que tu mette en place, dans cette étape, au MINIMUM deux "familles/variantes" de bateau (e.g. Speed et Tow, ou Sail et House, tu choisis) et au MINIMUM deux variantes visuelles dans ces familles.  
  
Je pense qu'on peut utiliser les [Prefab Variant](https://docs.unity3d.com/Manual/PrefabVariants.html) pour ça ?  
Avec ça en place je pourrais voir comment tu t'es débrouillée et je pourrais reprendre la main derrière.  
  
## 2.a Répétition de la même étape  
  
Vu que ce sont des modèles réduit je modifie la taille du modèle à 40% de sa taille initiale (0.4 en échelle x/y/z).  
Le soucis c'est que je dois le faire à chaque fois que je créé un nouveau Prefab !  
1. Dupliquer  le Prefab "Boat Template" (qui se trouve dans le dossier Prefabs).
2. Le renommer avec le nom du nouveau bateau que j'aimerais rajouter.
3. L'ouvrir pour l'éditer (double clic dessus).
4. Cliquer/déposer le modèle du bateau en enfant du GameObject parent.
5. **Redimensionner le modèle à 40%.**  
  
:information_source:  *Tu peux regarder les prefabs que j'ai déjà créé pour comprendre mon processus.*
  
Je pourrais modifier l'échelle racine de mon Prefab mais **il est recommandé de ne pas modifier l'échelle du transform "racine" d'une entité !**  
Et surtout si jamais on décide de changer l'échelle du projet (à 30 ou 60% par exemple) je vais devoir le refaire sur tout mes Prefabs ?!  

- [ ] Il faudrait trouver un moyen simple que cette échelle soit prédéfinie en amont et à un seul endroit.
  
Tiens d'ailleurs une fois que tu aura fais ça est-ce que tu peux **réduire la taille des bateaux à 37.5%** ?  
  
**:information_source: Une fois que c'est fait : `commit`, `push` et passe à la suite :smile:**  
  
## 2.b Répétition de la même étape (futur) (bis)  
  
J'imagine que quand j'aurais plusieurs variantes visuelles d'une même famille de bateau (le "Boat Speed" en à 10 !) je devrais à chaque fois lui redonner ses variables de comportement.  
Non seulement c'est répétitif, créateur d'erreur potentiel mais j'aimerais en plus m'assurer qu'ils aient "véritablement" le même comportement commun.  
Si je décide que "Boat Speed A" et "Boat Speed B" ont le même comportement et que je modifie le comportement de la variante A j'aimerais que la variante B soit aussi affectée/modifiée.  
  
**Est-ce que tu pourrais configurer les Prefabs d'une même famille pour que le comportement d'une variante visuel soit définie en amont de cette dernière ?**  
  
  - [ ] Que toutes les variantes visuelles d'une même famille aient le même comportement.  
  
**:information_source: Une fois que c'est fait : `commit`, `push` et passe à la suite :smile:**  
  
## 3. Fini la préparation, place à l'action !  
  
Maintenant tu devrais avoir quelques familles et variantes visuelles de bateau de prêt ?  
J'ai commencé à modifier le script qui s'occupe de faire apparaitre les instances de Prefab, `BoatManager.cs`, mais j'ai deux problèmes.  
  
### 3.a Toutes ces références en dure me rendent fou...  
  
Comme tu peux le voir dans le script `BoatManager.cs` j'ai commencé à rajouter deux autres variables/références pour les Prefab "Boat House B" et "Boat House C" (habilement nommées `boatHouseB` et `boatHouseC`).  
Mais on ne va pas faire ça pour tout nos Prefab ?!  
Il devrait y avoir un moyen de réunir ces familles dans une grande collection; non ?  
- [ ] Est-ce que tu aurais une solution pour ne plus qu'on ai besoin de modifier le script à chaque fois qu'on a une nouvelle variante de bateau ?  
  
**:information_source: Une fois que c'est fait : `commit`, `push` et passe à la suite :smile:**  
  
### 3.b Am, stram, gram, pic et pic et colégram...  
  
J'ai aussi rajouté une méthode pour choisir un bateau au hasard, appelée `GetRandomBoat()` dans `BoatManager.cs`.  
Le soucis c'est que non seulement je ne veux pas écrire des `if {} else if {}` à l'infini mais en plus si on réuni toutes nos variantes dans une collection le script ne saura plus spécifiquement le nombre de variantes ?  
Enfin si, il pourra le savoir via la propriété `Length` ou `Count` mais ça nous donne juste un nombre entier.  
- [ ] Il faudrait pouvoir choisir un nombre au hasard et *pointer son index* sur une variante et la renvoyer pour qu'elle soit instanciée.  
  
**:information_source: Une fois que c'est fait : `commit`, `push` et passe à la suite :smile:**  
  
### 3.b  (Bonus) Chance pondérée  
  
Ok, si tu es arrivé jusqu'ici déjà bravo 👏  
  
En bonus, est-ce que tu pense que tu pourrais implémenter un principe de **chance pondérée** (aussi appelée [fonction poids](https://fr.wikipedia.org/wiki/Fonction_poids)) sur la sélection aléatoire de nos bateaux ?  
  
C.a.d que plutôt que d'avoir une simple collection de nos bateaux (et qu'ils aient la même chance d'être sélectionnés au hasard), on aurait une collection avec des pairs `[Boat, Weight]` qui indiqueraient la chance relative de chaque bateau d'apparaitre.  
  
**:information_source: Une fois que c'est fait : `commit`, `push` et passe à la suite :smile:**  
  
### 4. (Bonus) SHOW ME WHAT YOU GOT
  
  En bonus, est-ce que tu pourrais mettre à jour le gif (prononcé gif) du projet dans ce README (ligne 9, tout en haut) ?  
  Les deux fichiers sont à la racine du projet, appelés "README.md" et "model_boat_club_simulator_preview.gif".  
  J'utilise [ScreenToGif](https://www.screentogif.com/) mais tu peux utiliser le logiciel que tu souhaite.  

**:information_source: Une fois que c'est fait : `commit`, `push` et passe à la suite :smile:**  
