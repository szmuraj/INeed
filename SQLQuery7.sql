BEGIN TRANSACTION;

-- 1. CZYSZCZENIE TABEL (Reset)
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
    'Kwestionariusz Oceny Potrzeb', 
    'Needs Assessment Questionnaire', 
    'Poznaj swoje kluczowe motywatory w pracy i życiu.', 
    'Discover your key motivators in work and life.', 
    'Badanie określa nasilenie czterech podstawowych potrzeb: Osiągnięć, Afiliacji (przynależności), Autonomii oraz Dominacji. Wyniki pomogą Ci lepiej dobrać środowisko pracy.', 
    'The survey determines the intensity of four basic needs: Achievement, Affiliation, Autonomy, and Dominance. results will help you choose a better work environment.', 
    'KOP.png', 1);
SET IDENTITY_INSERT [dbo].[Forms] OFF;

-- 3. WSTAW KATEGORIE Z PEŁNYM DORADZTWEM
-- Tutaj znajdują się opisy dla poziomów: Niski (1-4), Średni (5-6), Wysoki (7-10)

INSERT INTO [dbo].[Categories] 
(Id, Name, NameEN, Code, Color, StenNormsFemale, StenNormsMale, 
 AdviceLow, AdviceAvg, AdviceHigh, 
 AdviceLowEN, AdviceAvgEN, AdviceHighEN)
VALUES 
(
    '11111111-1111-1111-1111-111111111111', 'Potrzeba Osiągnięć', 'Need for Achievement', 'ACHIEVEMENT', '#76FF03', 
    '14,15,17,18,19,21,22,23,24,25', '12,15,17,18,19,21,22,23,24,25',
    
    -- PL
    'Twoja potrzeba osiągnięć jest niska. W pracy cenisz sobie spokój, przewidywalność i brak nadmiernej presji. Unikasz rywalizacji i wyścigu szczurów. Najlepiej sprawdzisz się w zadaniach stałych, rutynowych, gdzie cele są łatwo osiągalne, a ryzyko porażki minimalne.',
    'Twoja potrzeba osiągnięć jest na przeciętnym poziomie. Lubisz odnosić sukcesy i rozwijać się, ale zachowujesz zdrowy balans między pracą a życiem prywatnym. Potrafisz się zmobilizować do trudniejszych zadań, ale nie za wszelką cenę.',
    'Masz bardzo wysoką potrzebę osiągnięć. Jesteś osobą ambitną, nastawioną na cel i ciągły rozwój. Lubisz wyzwania i rywalizację. Najlepiej czujesz się w środowisku, które nagradza wyniki i pozwala Ci pokonywać własne ograniczenia.',
    
    -- EN
    'Your need for achievement is low. You value peace, predictability, and lack of excessive pressure at work. You avoid competition. You will perform best in routine tasks where goals are easily achievable and the risk of failure is minimal.',
    'Your need for achievement is average. You enjoy success and development, but you maintain a healthy work-life balance. You can mobilize yourself for difficult tasks, but not at any cost.',
    'You have a very high need for achievement. You are ambitious, goal-oriented, and focused on continuous growth. You enjoy challenges and competition. You thrive in environments that reward results.'
),
(
    '22222222-2222-2222-2222-222222222222', 'Potrzeba Afiliacji', 'Need for Affiliation', 'AFFILIATION', '#26A69A', 
    '6,8,10,11,12,13,14,16,17,20', '6,8,9,10,11,13,14,15,18,20',
    
    -- PL
    'Twoja potrzeba afiliacji jest niska. Jesteś osobą niezależną, zadaniową, która potrafi pracować w samotności. Relacje w pracy są dla Ciebie drugoplanowe. Uważaj, aby nie być postrzeganym jako osoba chłodna lub niedostępna.',
    'Twoja potrzeba afiliacji jest umiarkowana. Lubisz ludzi i dobrze czujesz się w zespole, ale potrzebujesz też czasu na pracę indywidualną. Utrzymujesz poprawne relacje, nie uzależniając od nich swojej samooceny.',
    'Masz wysoką potrzebę afiliacji (przynależności). Relacje z ludźmi są dla Ciebie kluczowe. Doskonale sprawdzisz się w pracy zespołowej, obsłudze klienta i wszędzie tam, gdzie liczy się empatia i kontakt z drugim człowiekiem.',
    
    -- EN
    'Your need for affiliation is low. You are an independent, task-oriented person who can work alone. Relationships at work are secondary to you. Be careful not to be perceived as cold or inaccessible.',
    'Your need for affiliation is moderate. You like people and feel good in a team, but you also need time for individual work. You maintain correct relationships without making your self-esteem dependent on them.',
    'You have a high need for affiliation. Relationships with people are crucial for you. You will excel in teamwork, customer service, and anywhere empathy and human contact are important.'
),
(
    '33333333-3333-3333-3333-333333333333', 'Potrzeba Autonomii', 'Need for Autonomy', 'AUTONOMY', '#FFF700', 
    '14,15,17,18,19,21,22,23,24,25', '13,16,17,18,20,21,22,23,24,25',
    
    -- PL
    'Niska potrzeba autonomii. Wolisz jasne wytyczne, procedury i obecność przełożonego, który pokieruje Twoją pracą. Źle czujesz się w sytuacjach niepewnych, wymagających dużej samodzielności decyzyjnej.',
    'Przeciętna potrzeba autonomii. Cenisz sobie pewną swobodę w działaniu, ale akceptujesz też nadzór i ramy narzucone przez organizację. Potrafisz pracować samodzielnie, konsultując kluczowe decyzje.',
    'Wysoka potrzeba autonomii. Jesteś "wolnym strzelcem". Nie znosisz mikrozarządzania i ścisłej kontroli. Najlepiej pracujesz, gdy sam ustalasz swój harmonogram i metody działania. Rozważ pracę projektową lub własną działalność.',
    
    -- EN
    'Low need for autonomy. You prefer clear guidelines, procedures, and the presence of a supervisor to direct your work. You feel uncomfortable in uncertain situations requiring high decision-making independence.',
    'Average need for autonomy. You value some freedom of action, but you also accept supervision and frameworks imposed by the organization. You can work independently while consulting on key decisions.',
    'High need for autonomy. You hate micromanagement and strict control. You work best when you set your own schedule and methods. Consider project-based work or freelancing.'
),
(
    '44444444-4444-4444-4444-444444444444', 'Potrzeba Dominacji', 'Need for Dominance', 'DOMINANCE', '#D32F2F', 
    '7,9,11,13,14,16,18,21,22,25', '8,11,12,14,16,17,19,21,23,25',
    
    -- PL
    'Niska potrzeba dominacji. Wolisz rolę wykonawcy lub eksperta niż lidera. Unikasz konfrontacji i nie dążysz do władzy nad innymi. Dobrze czujesz się w strukturach poziomych, partnerskich.',
    'Umiarkowana potrzeba dominacji. Potrafisz przejąć inicjatywę i przewodzić grupie, gdy sytuacja tego wymaga, ale nie dążysz do władzy za wszelką cenę. Jesteś dobrym kandydatem na lidera merytorycznego.',
    'Wysoka potrzeba dominacji. Jesteś naturalnym liderem. Lubisz mieć wpływ na innych, podejmować decyzje i kontrolować sytuację. Uważaj, aby Twoja asertywność nie zamieniła się w autorytaryzm.',
    
    -- EN
    'Low need for dominance. You prefer the role of an executor or expert rather than a leader. You avoid confrontation and do not seek power over others. You feel good in horizontal, partner-like structures.',
    'Moderate need for dominance. You can take initiative and lead a group when the situation requires it, but you do not seek power at all costs. You are a good candidate for a substantive leader.',
    'High need for dominance. You are a natural leader. You like to influence others, make decisions, and control the situation. Be careful that your assertiveness does not turn into authoritarianism.'
);

-- 4. WSTAW PYTANIA (Dla FormId = 1)
DECLARE @FID INT = 1;

INSERT INTO [dbo].[Questions] ([QuestionId], [FormId], [Number], [Query], [QueryEN], [Category]) VALUES
(NEWID(), @FID, 1, 'W pracy staram się dawać z siebie wszystko.', 'I strive to give my best at work.', 'Potrzeba Osiągnięć'),
(NEWID(), @FID, 2, 'Spędzam dużo czasu, rozmawiając z innymi ludźmi.', 'I spend a lot of time talking to other people.', 'Potrzeba Afiliacji'),
(NEWID(), @FID, 3, 'Chciałbym mieć wolną rękę, wykonując swoje obowiązki.', 'I would like to have a free hand in performing my duties.', 'Potrzeba Autonomii'),
(NEWID(), @FID, 4, 'Miałbym satysfakcję, gdybym sprawował nadzór nad jakimś zadaniem.', 'I would be satisfied if I supervised a task.', 'Potrzeba Dominacji'),
(NEWID(), @FID, 5, 'Ciężko pracuję.', 'I work hard.', 'Potrzeba Osiągnięć'),
(NEWID(), @FID, 6, 'Jestem osobą towarzyską.', 'I am a sociable person.', 'Potrzeba Afiliacji'),
(NEWID(), @FID, 7, 'Chciałbym mieć taką pracę, w której samodzielnie ustalam harmonogram zajęć.', 'I would like to have a job where I set my own schedule.', 'Potrzeba Autonomii'),
(NEWID(), @FID, 8, 'Wolałbym raczej otrzymywać polecenia, niż je wydawać.', 'I would rather receive orders than give them.', 'Potrzeba Dominacji'),
(NEWID(), @FID, 9, 'Ważne jest dla mnie, by jak najlepiej wykonywać swoją pracę.', 'It is important for me to perform my work as best as possible.', 'Potrzeba Osiągnięć'),
(NEWID(), @FID, 10, 'Gdy mam wybór, wolę pracować z grupą niż samemu.', 'When I have a choice, I prefer working with a group than alone.', 'Potrzeba Afiliacji'),
(NEWID(), @FID, 11, 'Chciałbym być swoim własnym szefem.', 'I would like to be my own boss.', 'Potrzeba Autonomii'),
(NEWID(), @FID, 12, 'Dążę do tego, żeby przewodzić innym.', 'I strive to lead others.', 'Potrzeba Dominacji'),
(NEWID(), @FID, 13, 'Wywieram na siebie presję, by stać się tym, kim mogę być.', 'I put pressure on myself to become who I can be.', 'Potrzeba Osiągnięć'),
(NEWID(), @FID, 14, 'Wolę wykonywać swoją pracę samodzielnie i pozwalam innym działać w ten sam sposób.', 'I prefer to do my work independently and let others do the same.', 'Potrzeba Afiliacji'),
(NEWID(), @FID, 15, 'Nad zadaniami w pracy lubię pracować w swoim własnym tempie.', 'I like to work on tasks at my own pace.', 'Potrzeba Autonomii'),
(NEWID(), @FID, 16, 'Postrzegam się jako osobę organizacyjną i kierującą pracą innych.', 'I see myself as an organized person who directs the work of others.', 'Potrzeba Dominacji'),
(NEWID(), @FID, 17, 'Bardzo się staram poprawić swoje wcześniejsze wyniki w pracy.', 'I try very hard to improve my previous results at work.', 'Potrzeba Osiągnięć'),
(NEWID(), @FID, 18, 'Wykonując zadania w pracy, staram się być swoim własnym szefem.', 'When performing tasks at work, I try to be my own boss.', 'Potrzeba Autonomii'),
(NEWID(), @FID, 19, 'Gdy pracuję z grupą ludzi, to usiłuję nimi dowodzić.', 'When I work with a group of people, I attempt to lead them.', 'Potrzeba Dominacji');

-- 5. WSTAW ODPOWIEDZI (Z poprawionymi kolorami i logiką punktacji)
INSERT INTO [dbo].[Answers] ([AnswerId], [QuestionId], [Reply], [ReplyEN], [Score], [Order], [Color])
SELECT 
    NEWID(), 
    q.QuestionId, 
    t.Reply, 
    t.ReplyEN, 
    -- Logika odwracania punktacji (8 i 14)
    CASE WHEN q.Number IN (8, 14) THEN (6 - t.Score) ELSE t.Score END, 
    t.[Order], 
    t.Color
FROM [dbo].[Questions] q
CROSS JOIN (
    VALUES 
    ('Zdecydowanie się nie zgadzam', 'Strongly Disagree', 1, 1, '#A00000'),
    ('Nie zgadzam się', 'Disagree', 2, 2, '#FF0000'),
    ('Trudno powiedzieć', 'Hard to say', 3, 3, '#777777'),
    ('Zgadzam się', 'Agree', 4, 4, '#00AA00'),
    ('Zdecydowanie się zgadzam', 'Strongly Agree', 5, 5, '#008000')
) AS t(Reply, ReplyEN, Score, [Order], Color)
WHERE q.FormId = @FID;

COMMIT;