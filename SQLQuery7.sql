BEGIN TRANSACTION;

-- 1. CZYSZCZENIE TABEL
DELETE FROM [dbo].[VisitorCategoryScores];
DELETE FROM [dbo].[VisitorResults];
DELETE FROM [dbo].[Answers];
DELETE FROM [dbo].[Questions];
DELETE FROM [dbo].[Forms];
DELETE FROM [dbo].[Categories];

-- 2. WSTAW FORMULARZ (KOP)
SET IDENTITY_INSERT [dbo].[Forms] ON;

INSERT INTO [dbo].[Forms] ([Id], [Title], [TitleEN], [DecryptionShort], [DecryptionShortEN], [DecryptionLong], [DecryptionLongEN], [Graphic], [IsActive]) 
VALUES (1, 
    N'Kwestionariusz Oceny Potrzeb', 
    N'Needs Assessment Questionnaire', 
    N'Poznaj swoje kluczowe motywatory w pracy i życiu.', 
    N'Discover your key motivators in work and life.', 
    N'Badanie określa nasilenie czterech podstawowych potrzeb: Osiągnięć, Afiliacji (przynależności), Autonomii oraz Dominacji. Wyniki pomogą Ci lepiej dobrać środowisko pracy.', 
    N'The survey determines the intensity of four basic needs: Achievement, Affiliation, Autonomy, and Dominance. results will help you choose a better work environment.', 
    N'KOP.png', 1);

SET IDENTITY_INSERT [dbo].[Forms] OFF;

-- 3. WSTAW KATEGORIE
INSERT INTO [dbo].[Categories] 
(Id, Name, NameEN, Code, Color, StenNormsFemale, StenNormsMale, 
 AdviceLow, AdviceAvg, AdviceHigh, 
 AdviceLowEN, AdviceAvgEN, AdviceHighEN)
VALUES 
(
    '11111111-1111-1111-1111-111111111111', N'Potrzeba Osiągnięć', N'Need for Achievement', N'ACHIEVEMENT', N'#76FF03', 
    N'14,15,17,18,19,21,22,23,24,25', N'12,15,17,18,19,21,22,23,24,25',
    
    -- PL
    N'Twoja potrzeba osiągnięć jest niska. W pracy cenisz sobie spokój, przewidywalność i brak nadmiernej presji. Unikasz rywalizacji i wyścigu szczurów. Najlepiej sprawdzisz się w zadaniach stałych, rutynowych, gdzie cele są łatwo osiągalne, a ryzyko porażki minimalne.',
    N'Twoja potrzeba osiągnięć jest na przeciętnym poziomie. Lubisz odnosić sukcesy i rozwijać się, ale zachowujesz zdrowy balans między pracą a życiem prywatnym. Potrafisz się zmobilizować do trudniejszych zadań, ale nie za wszelką cenę.',
    N'Masz bardzo wysoką potrzebę osiągnięć. Jesteś osobą ambitną, nastawioną na cel i ciągły rozwój. Lubisz wyzwania i rywalizację. Najlepiej czujesz się w środowisku, które nagradza wyniki i pozwala Ci pokonywać własne ograniczenia.',
    
    -- EN
    N'Your need for achievement is low. You value peace, predictability, and lack of excessive pressure at work. You avoid competition. You will perform best in routine tasks where goals are easily achievable and the risk of failure is minimal.',
    N'Your need for achievement is average. You enjoy success and development, but you maintain a healthy work-life balance. You can mobilize yourself for difficult tasks, but not at any cost.',
    N'You have a very high need for achievement. You are ambitious, goal-oriented, and focused on continuous growth. You enjoy challenges and competition. You thrive in environments that reward results.'
),
(
    '22222222-2222-2222-2222-222222222222', N'Potrzeba Afiliacji', N'Need for Affiliation', N'AFFILIATION', N'#26A69A', 
    N'6,8,10,11,12,13,14,16,17,20', N'6,8,9,10,11,13,14,15,18,20',
    
    -- PL
    N'Twoja potrzeba afiliacji jest niska. Jesteś osobą niezależną, zadaniową, która potrafi pracować w samotności. Relacje w pracy są dla Ciebie drugoplanowe. Uważaj, aby nie być postrzeganym jako osoba chłodna lub niedostępna.',
    N'Twoja potrzeba afiliacji jest umiarkowana. Lubisz ludzi i dobrze czujesz się w zespole, ale potrzebujesz też czasu na pracę indywidualną. Utrzymujesz poprawne relacje, nie uzależniając od nich swojej samooceny.',
    N'Masz wysoką potrzebę afiliacji (przynależności). Relacje z ludźmi są dla Ciebie kluczowe. Doskonale sprawdzisz się w pracy zespołowej, obsłudze klienta i wszędzie tam, gdzie liczy się empatia i kontakt z drugim człowiekiem.',
    
    -- EN
    N'Your need for affiliation is low. You are an independent, task-oriented person who can work alone. Relationships at work are secondary to you. Be careful not to be perceived as cold or inaccessible.',
    N'Your need for affiliation is moderate. You like people and feel good in a team, but you also need time for individual work. You maintain correct relationships without making your self-esteem dependent on them.',
    N'You have a high need for affiliation. Relationships with people are crucial for you. You will excel in teamwork, customer service, and anywhere empathy and human contact are important.'
),
(
    '33333333-3333-3333-3333-333333333333', N'Potrzeba Autonomii', N'Need for Autonomy', N'AUTONOMY', N'#FFF700', 
    N'14,15,17,18,19,21,22,23,24,25', N'13,16,17,18,20,21,22,23,24,25',
    
    -- PL
    N'Niska potrzeba autonomii. Wolisz jasne wytyczne, procedury i obecność przełożonego, który pokieruje Twoją pracą. Źle czujesz się w sytuacjach niepewnych, wymagających dużej samodzielności decyzyjnej.',
    N'Przeciętna potrzeba autonomii. Cenisz sobie pewną swobodę w działaniu, ale akceptujesz też nadzór i ramy narzucone przez organizację. Potrafisz pracować samodzielnie, konsultując kluczowe decyzje.',
    N'Wysoka potrzeba autonomii. Jesteś "wolnym strzelcem". Nie znosisz mikrozarządzania i ścisłej kontroli. Najlepiej pracujesz, gdy sam ustalasz swój harmonogram i metody działania. Rozważ pracę projektową lub własną działalność.',
    
    -- EN
    N'Low need for autonomy. You prefer clear guidelines, procedures, and the presence of a supervisor to direct your work. You feel uncomfortable in uncertain situations requiring high decision-making independence.',
    N'Average need for autonomy. You value some freedom of action, but you also accept supervision and frameworks imposed by the organization. You can work independently while consulting on key decisions.',
    N'High need for autonomy. You hate micromanagement and strict control. You work best when you set your own schedule and methods. Consider project-based work or freelancing.'
),
(
    '44444444-4444-4444-4444-444444444444', N'Potrzeba Dominacji', N'Need for Dominance', N'DOMINANCE', N'#D32F2F', 
    N'7,9,11,13,14,16,18,21,22,25', N'8,11,12,14,16,17,19,21,23,25',
    
    -- PL
    N'Niska potrzeba dominacji. Wolisz rolę wykonawcy lub eksperta niż lidera. Unikasz konfrontacji i nie dążysz do władzy nad innymi. Dobrze czujesz się w strukturach poziomych, partnerskich.',
    N'Umiarkowana potrzeba dominacji. Potrafisz przejąć inicjatywę i przewodzić grupie, gdy sytuacja tego wymaga, ale nie dążysz do władzy za wszelką cenę. Jesteś dobrym kandydatem na lidera merytorycznego.',
    N'Wysoka potrzeba dominacji. Jesteś naturalnym liderem. Lubisz mieć wpływ na innych, podejmować decyzje i kontrolować sytuację. Uważaj, aby Twoja asertywność nie zamieniła się w autorytaryzm.',
    
    -- EN
    N'Low need for dominance. You prefer the role of an executor or expert rather than a leader. You avoid confrontation and do not seek power over others. You feel good in horizontal, partner-like structures.',
    N'Moderate need for dominance. You can take initiative and lead a group when the situation requires it, but you do not seek power at all costs. You are a good candidate for a substantive leader.',
    N'High need for dominance. You are a natural leader. You like to influence others, make decisions, and control the situation. Be careful that your assertiveness does not turn into authoritarianism.'
);

-- 4. WSTAW PYTANIA
DECLARE @FID INT = 1;

INSERT INTO [dbo].[Questions] ([QuestionId], [FormId], [Number], [Query], [QueryEN], [Category]) VALUES
(NEWID(), @FID, 1, N'W pracy staram się dawać z siebie wszystko.', N'I strive to give my best at work.', N'Potrzeba Osiągnięć'),
(NEWID(), @FID, 2, N'Spędzam dużo czasu, rozmawiając z innymi ludźmi.', N'I spend a lot of time talking to other people.', N'Potrzeba Afiliacji'),
(NEWID(), @FID, 3, N'Chciałbym mieć wolną rękę, wykonując swoje obowiązki.', N'I would like to have a free hand in performing my duties.', N'Potrzeba Autonomii'),
(NEWID(), @FID, 4, N'Miałbym satysfakcję, gdybym sprawował nadzór nad jakimś zadaniem.', N'I would be satisfied if I supervised a task.', N'Potrzeba Dominacji'),
(NEWID(), @FID, 5, N'Ciężko pracuję.', N'I work hard.', N'Potrzeba Osiągnięć'),
(NEWID(), @FID, 6, N'Jestem osobą towarzyską.', N'I am a sociable person.', N'Potrzeba Afiliacji'),
(NEWID(), @FID, 7, N'Chciałbym mieć taką pracę, w której samodzielnie ustalam harmonogram zajęć.', N'I would like to have a job where I set my own schedule.', N'Potrzeba Autonomii'),
(NEWID(), @FID, 8, N'Wolałbym raczej otrzymywać polecenia, niż je wydawać.', N'I would rather receive orders than give them.', N'Potrzeba Dominacji'),
(NEWID(), @FID, 9, N'Ważne jest dla mnie, by jak najlepiej wykonywać swoją pracę.', N'It is important for me to perform my work as best as possible.', N'Potrzeba Osiągnięć'),
(NEWID(), @FID, 10, N'Gdy mam wybór, wolę pracować w grupie niż samemu.', N'When I have a choice, I prefer working with a group than alone.', N'Potrzeba Afiliacji'),
(NEWID(), @FID, 11, N'Chciałbym być swoim własnym szefem.', N'I would like to be my own boss.', N'Potrzeba Autonomii'),
(NEWID(), @FID, 12, N'Dążę do tego, żeby przewodzić innym.', N'I strive to lead others.', N'Potrzeba Dominacji'),
(NEWID(), @FID, 13, N'Wywieram na siebie presję, by stać się tym, kim mogę być.', N'I put pressure on myself to become who I can be.', N'Potrzeba Osiągnięć'),
(NEWID(), @FID, 14, N'Wolę wykonywać swoją pracę samodzielnie i pozwalam innym działać w ten sam sposób.', N'I prefer to do my work independently and let others do the same.', N'Potrzeba Afiliacji'),
(NEWID(), @FID, 15, N'Nad zadaniami w pracy lubię pracować w swoim własnym tempie.', N'I like to work on tasks at my own pace.', N'Potrzeba Autonomii'),
(NEWID(), @FID, 16, N'Postrzegam się jako osobę organizacyjną i kierującą pracą innych.', N'I see myself as an organized person who directs the work of others.', N'Potrzeba Dominacji'),
(NEWID(), @FID, 17, N'Bardzo się staram poprawić swoje wcześniejsze wyniki w pracy.', N'I try very hard to improve my previous results at work.', N'Potrzeba Osiągnięć'),
(NEWID(), @FID, 18, N'Wykonując zadania w pracy, staram się być swoim własnym szefem.', N'When performing tasks at work, I try to be my own boss.', N'Potrzeba Autonomii'),
(NEWID(), @FID, 19, N'Gdy pracuję z grupą ludzi, to usiłuję nimi dowodzić.', N'When I work with a group of people, I attempt to lead them.', N'Potrzeba Dominacji');

-- 5. WSTAW ODPOWIEDZI
INSERT INTO [dbo].[Answers] ([AnswerId], [QuestionId], [Reply], [ReplyEN], [Score], [Order], [Color])
SELECT 
    NEWID(), 
    q.QuestionId, 
    t.Reply, 
    t.ReplyEN, 
    CASE WHEN q.Number IN (8, 14) THEN (6 - t.Score) ELSE t.Score END, 
    t.[Order], 
    t.Color
FROM [dbo].[Questions] q
CROSS JOIN (
    VALUES 
    (N'Zdecydowanie się nie zgadzam', N'Strongly Disagree', 1, 1, N'#A00000'),
    (N'Nie zgadzam się', N'Disagree', 2, 2, N'#FF0000'),
    (N'Trudno powiedzieć', N'Hard to say', 3, 3, N'#777777'),
    (N'Zgadzam się', N'Agree', 4, 4, N'#00AA00'),
    (N'Zdecydowanie się zgadzam', N'Strongly Agree', 5, 5, N'#008000')
) AS t(Reply, ReplyEN, Score, [Order], Color)
WHERE q.FormId = @FID;

COMMIT;