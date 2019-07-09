//
//  TrackerAdvance.h
//  DOT
//
//  Created by Woncheol Heo on 2019. 3. 28..
//  Copyright © 2019년 wisetracker. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <DOTSession/DOTSession.h>
#import <DOTSession/Tracker.h>
#import "Conversion.h"
#import "RevenueJson.h"
#import "Purchase.h"
#import "Click.h"
#import "Page.h"
#import "ClickJson.h"
#import "RevenueJson.h"
#import "GoalJson.h"
#import "PagesJson.h"

NS_ASSUME_NONNULL_BEGIN
@interface TrackerAdvance : NSObject

@property (nonatomic) GoalJson *goalJson;
@property (nonatomic) RevenueJson *revenueJson;
@property (nonatomic) ClickJson *clickJson;
@property (nonatomic) PagesJson *pagesJson;

+ (TrackerAdvance *)sharedInstance;
- (void)onStartPage;
- (double)onStopPage;
- (void)setGoalJosnWithConversion:(Conversion *)conversion;
- (void)setRevenueJsonWithPurchase:(Purchase *)purchase;
- (void)setClickJsonWithClick:(Click *)click;
- (void)setPagesJsonWithPage:(Page *)page;

- (BOOL)sendTransaction;
- (BOOL)sendTransactionByPage;
- (BOOL)checkPurchase;
- (void)updateBeforePurchase;
- (void)updateAfterPurchase;

- (void)enterForeground;
- (void)enterBackground;

- (void)injectJSinWebview:(UIWebView *)webView;
- (void)injectJSInWkWebView:(WKWebView *)wkWebView;

//WebTracker 호출함수
- (BOOL)getDOTInitFlag;
- (BOOL)checkNewSession;
- (void)occurNewSessionWithType:(NSInteger)type;
- (void)createSessionJson;
- (void)createClickJson;
- (void)createGoalJson;
- (void)createRevenueJson;
- (void)createPagesJson;
- (void)createEntireJson;
- (void)createEntireJson2;
@end

NS_ASSUME_NONNULL_END
